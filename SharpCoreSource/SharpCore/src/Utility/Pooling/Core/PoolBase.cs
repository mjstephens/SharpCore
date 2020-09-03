using System.Collections.Generic;

namespace SharpCore.Utility.Pooling
{
    public abstract class PoolBase : IPool
    {
        #region Properties

        public int capacityMin
        {
            get => _capacityMin;
            set => SetCapacityMin(value);
        }

        public int capacityMax
        {
            get => _capacityMax;
            set => SetCapacityMax(value);
        }

        public int instanceCount => _pool.Count;
        
        public int activeCount => GetPoolActiveCount();
        
        public string poolLabel { get; set; }

        #endregion Properties


        #region Fields

        /// <summary>
        /// The pool of instances, ordered from oldest (0) to newest (count - 1)
        /// </summary>
        private readonly List<IClientPoolable> _pool;

        // Backing fields for properties
        private int _capacityMin;
        private int _capacityMax = -1;

        #endregion Fields


        #region Constructor
        
        protected PoolBase()
        {
            // Set default capacity (no prewarmed, infinite max)
            _pool = new List<IClientPoolable>();
        }

        #endregion Constructor


        #region Get Next
        
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
                    if (_capacityMax > 0 && _pool.Count >= _capacityMax)
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
            return _pool[0].availableInPool ? _pool[0] : null;
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

        public void ClaimInstance(IClientPoolable instance)
        {
            _pool.Remove(instance);
            _pool.Add(instance);
            instance.availableInPool = false;
            instance.Claim();
        }

        public void RelinquishInstance(IClientPoolable instance)
        {
            _pool.Remove(instance);
            _pool.Insert(0, instance);
            instance.availableInPool = true;
            instance.Relinquish();
        }
        
        void IPool.DeleteFromInstance(IClientPoolable instance)
        {
            _pool.Remove(instance);
        }

        #endregion Ownership


        #region Capactiy

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        private void SetCapacityMin(int minValue)
        {
            _capacityMin = minValue;
            if (!PoolValidationUtility.ValidatePoolCapacity(_capacityMin, _capacityMax))
                return;
            
            // We need to create objects if we don't have enough
            EnforceMinimumCapacity();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue"></param>
        private void SetCapacityMax(int maxValue)
        {
            _capacityMax = maxValue;
            if (!PoolValidationUtility.ValidatePoolCapacity(_capacityMin, _capacityMax))
                return;
            
            // We need to remove items if we have too many
            EnforceMaximumCapacity();
        }

        /// <summary>
        /// Ensures that there are a minimum number of instaces in the pool
        /// </summary>
        private void EnforceMinimumCapacity()
        {
            int createCount = _capacityMin - _pool.Count;
            for (int i = 0; i < createCount; i++)
            {
                CreateNew(false);
            }
        }

        /// <summary>
        /// Ensures that instances are destroyed if the instance count exceeds the pool max capacity
        /// </summary>
        private void EnforceMaximumCapacity()
        {
            int removalCount = _pool.Count - _capacityMax;
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

        void IPool.Clean()
        {
            // Determine number of instances to clean
            int cleanCount = 0;
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].availableInPool)
                {
                    cleanCount++;
                }
                else
                {
                    break;
                }
            }
            
            // Clean instances
            int cleaned = 0;
            while (cleaned < cleanCount)
            {
                _pool[0].DeleteFromPool();
                _pool.RemoveAt(0);
                cleaned++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetPoolActiveCount()
        {
            int a = 0;
            for (int i = _pool.Count - 1; i >= 0; i--)
            {
                if (!_pool[i].availableInPool)
                {
                    a++;
                }                
                else
                {
                    break;
                }
            }

            return a;
        }

        public bool InternalValidatePoolInstanceLayout(int targetAvailable)
        {
            bool pass = true;
            bool hasFoundActive = false;
            int availableCount = 0;
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].availableInPool)
                {
                    hasFoundActive = true;
                }
                else
                {
                    if (hasFoundActive)
                    {
                        pass = false;
                        break;
                    }
                    availableCount++;
                }
            }

            if (pass)
                pass = availableCount == targetAvailable;
            return pass;
        }

        #endregion Utility
    }
}