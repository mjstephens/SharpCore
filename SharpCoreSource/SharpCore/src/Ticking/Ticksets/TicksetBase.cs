using System.Collections.Generic;

namespace SharpCore.Ticking
{
    public abstract class TicksetBase<T> : ITicksetInstance where T : ITickClient
    {
        #region Variables

        // Tickset data
        protected TicksetConfigData ticksetData;

        /// <summary>
        /// The list of current clients subscribed to this tickset.
        /// </summary>
        protected readonly List<T> _current = new List<T>();
        public readonly List<T> stagedForRemoval = new List<T>();
        public readonly List<T> stagedForAddition = new List<T>();

        public int subscriberCount { get; private set; }

        public string ticksetName => ticksetData.ticksetName;

        #endregion Variables


        #region Tick

        /// <summary>
        /// 
        /// </summary>
        private void AddStagedTickables()
        {
            foreach (var tick in stagedForAddition)
            {
                _current.Add(tick);
            }

            subscriberCount = _current.Count;
            stagedForAddition.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FlushStagedTickables()
        {
            foreach (var tick in stagedForRemoval)
            {
                _current.Remove(tick);
            }

            subscriberCount = _current.Count;
            stagedForRemoval.Clear();
        }

        /// <summary>
        /// Iterates through and ticks every ITickable assigned to this tickset.
        /// </summary>
        public virtual void Tick(double delta)
        {
            // Add/remove staged ticks from group
            AddStagedTickables();
            FlushStagedTickables();
        }

        #endregion Tick
    }
}