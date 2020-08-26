namespace SharpCore.Utility.Pooling
{
    /// <summary>
    /// Defines pooling behaviors for client objects.
    /// </summary>
    public interface IClientPoolable
    {
        /// <summary>
        /// Creates a new instance and adds it to the pool.
        /// </summary>
        /// <param name="pool">The pool from which the instance was created</param>
        void OnInstanceCreated(PoolBase pool);
        
        /// <summary>
        /// Returns true if the poolable is available for use.
        /// </summary>
        /// <returns></returns>
        bool GetIsAvailable();
        
        /// <summary>
        /// Claims the poolable and activates for use.
        /// </summary>
        void Claim();
        
        /// <summary>
        /// Releases ownership and returns poolable to the pool for later use.
        /// </summary>
        void Relinquish();

        /// <summary>
        /// Recycles an instance (immediately reusing in diff context).
        /// </summary>
        void Recycle();

        /// <summary>
        /// Removes the poolable from the pool entirely.
        /// </summary>
        void DeleteFromPool();
    }
}