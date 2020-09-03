using SharpCore.Utility.Pooling;

namespace SharpCoreTests.Pooling.Demo
{
    /// <summary>
    /// Used for testing pooling functionality
    /// </summary>
    public class DemoPooledObjectInstance : IClientPoolable
    {
        #region Data

        public bool availableInPool { get; set; }
        
        private IPool _pool;

        #endregion Data
        
        
        #region Pooling

        void IClientPoolable.OnInstanceCreated(PoolBase pool)
        {
            _pool = pool;
        }

        void IClientPoolable.Claim()
        {
            
        }

        void IClientPoolable.Relinquish()
        {
            
        }

        void IClientPoolable.Recycle()
        {
            
        }

        void IClientPoolable.DeleteFromPool()
        {
            
        }

        #endregion Pooling


        #region Tests

        public void RelenquishFromInstance()
        {
            _pool.RelinquishInstance(this);
        }
        
        public void DeleteObjectInstance()
        {
            _pool.DeleteFromInstance(this);
        }

        #endregion Tests
    }
}