using System.Numerics;

namespace SharpCore.Utility.Pooling
{
    public interface IPool
    {
        #region Properties

        /// <summary>
        /// How many instances of the pooled object can be spawned before recycling starts.
        /// </summary>
        public Vector2 capacity { get; }

        /// <summary>
        /// The total umber of instances this pool currently contains.
        /// </summary>
        public int instanceCount { get; }
    
        /// <summary>
        /// The total number of ACTIVE instances this pool currently contains.
        /// </summary>
        public int activeCount { get; }
        
        /// <summary>
        /// The label for this pool; used for debugging.
        /// </summary>
        public string poolLabel { get; set; }

        #endregion Properties
    }
}