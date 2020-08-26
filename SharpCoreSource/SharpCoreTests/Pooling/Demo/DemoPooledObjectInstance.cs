using System;
using SharpCore.Utility.Pooling;

namespace SharpCoreTests.Pooling.Demo
{
    /// <summary>
    /// Used for testing pooling functionality
    /// </summary>
    public class DemoPooledObjectInstance : IClientPoolable
    {
        #region Pooling

        void IClientPoolable.OnInstanceCreated(PoolBase pool)
        {
            throw new NotImplementedException();
        }

        bool IClientPoolable.GetIsAvailable()
        {
            throw new NotImplementedException();
        }

        void IClientPoolable.Claim()
        {
            throw new NotImplementedException();
        }

        void IClientPoolable.Relinquish()
        {
            throw new NotImplementedException();
        }

        void IClientPoolable.Recycle()
        {
            throw new NotImplementedException();
        }

        void IClientPoolable.DeleteFromPool()
        {
            throw new NotImplementedException();
        }

        #endregion Pooling
    }
}