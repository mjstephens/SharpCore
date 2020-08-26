using System.Collections.Generic;
using System.Numerics;

namespace SharpCore.Utility.Pooling
{
    public abstract class PoolBase : IPool
    {
        #region Properties

        public Vector2 capacity { get; private set; }
        public int instanceCount => _pool.Count;
        public int activeCount => GetPoolActiveCount();
        public string poolLabel { get; set; }

        #endregion Properties


        #region Fields

        /// <summary>
        /// The pool of instances, ordered from oldest (0) to newest (count - 1)
        /// </summary>
        private readonly List<IClientPoolable> _pool;

        #endregion Fields


        #region Constructor

        /// <summary>
        /// Creates a new instance of the pool
        /// </summary>
        protected PoolBase()
        {
            // Set default capacity (no prewarmed, infinite max)
            _pool = new List<IClientPoolable>();
            SetCapacity(new Vector2(0, -1));
        }

        #endregion Constructor


        #region Get Next

        /// <summary>
        /// Claims and returns the next available instance from the pool.
        /// </summary>
        /// <returns></returns>
        public IClientPoolable GetNext()
        {
            // Get oldest pool object
            IClientPoolable result;
            if (_pool.Count > 0)
            {
                result = GetNextAvailable();
                if (result == null)
                {
                    // We are either full or at max capacity. Create or recycle
                    if (capacity.Y > 0 && _pool.Count >= capacity.Y)
                    {
                        result = Recycle();
                    }
                    else
                    {
                        result = CreateNew(true);
                    }
                }
                else
                {
                    ClaimInstance(result);
                }
            }
            else
            {
                result = CreateNew(true);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IClientPoolable GetNextAvailable()
        {
            return _pool[0].GetIsAvailable() ? _pool[0] : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IClientPoolable CreateNew(bool activateAfterCreation)
        {
            IClientPoolable result = CreateNewPoolable();
            result.OnInstanceCreated(this);

            // Claim (if using) or relinquish (if prewarming)
            if (activateAfterCreation)
            {
                ClaimInstance(result);
            }
            else
            {
                RelinquishInstance(result);
            }
            
            return result;
        }
        protected abstract IClientPoolable CreateNewPoolable();

        /// <summary>
        /// Recycles and claims the oldest instance of the pooled objects.
        /// </summary>
        /// <returns></returns>
        private IClientPoolable Recycle()
        {
            // Find oldest, relinquish, re-activate
            IClientPoolable p = _pool[0];
            _pool.RemoveAt(0);
            _pool.Add(p);
            p.Recycle();
            return p;
        }

        #endregion Get Next


        #region Ownership

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public void ClaimInstance(IClientPoolable instance)
        {
            _pool.Remove(instance);
            _pool.Add(instance);
            instance.Claim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public void RelinquishInstance(IClientPoolable instance)
        {
            _pool.Remove(instance);
            _pool.Insert(0, instance);
            instance.Relinquish();
        }
        
        /// <summary>
        /// Removes the instance from the pool entirely, called from the instance itself.
        /// Currently used only from pooled GameObjects (GameObjectPooledComponent).
        /// </summary>
        /// <param name="p"></param>
        public void DeleteFromInstance(IClientPoolable p)
        {
            _pool.Remove(p);
        }

        #endregion Ownership


        #region Capactiy

        /// <summary>
        /// Sets the maximum/minimum capacity of the pool.
        /// </summary>
        /// <param name="c"></param>
        public void SetCapacity(Vector2 c)
        {
            // Set min and max values
            SetCapacityMin((int)c.X);
            SetCapacityMax((int)c.Y);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        public void SetCapacityMin(int m)
        {
            capacity = new Vector2(m, capacity.Y);
            
            // We need to create objects if we don't have enough
            int createCount = m - _pool.Count;
            for (int i = 0; i < createCount; i++)
            {
                CreateNew(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        public void SetCapacityMax(int m)
        {
            capacity = new Vector2(capacity.X, m);
            
            // We need to remove items if we have too many
            int removalCount = _pool.Count - m;
            if (removalCount > 0)
            {
                // Remove oldest objects first
                for (int i = 0; i < _pool.Count; i++)
                {
                    if (removalCount <= 0)
                        break;
                
                    IClientPoolable p = _pool[i];
                    p.DeleteFromPool();
                    _pool.Remove(p);
                    removalCount--;
                }
            }
        }

        #endregion Capacity


        #region Utility

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetPoolActiveCount()
        {
            int a = 0;
            foreach (var p in _pool)
            {
                if (!p.GetIsAvailable())
                    a++;
            }

            return a;
        }

        #endregion Utility
    }
}