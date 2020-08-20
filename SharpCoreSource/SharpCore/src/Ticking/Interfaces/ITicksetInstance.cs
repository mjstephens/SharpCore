namespace SharpCore.Ticking.Interfaces
{
    public interface ITicksetInstance
    {
        #region Properties

        /// <summary>
        /// How many clients are current subscribed to this tickset.
        /// </summary>
        public int subscriberCount { get; }
        
        /// <summary>
        /// The display/debug name of this tickset
        /// </summary>
        public string ticksetName { get; }

        #endregion Properties
    }
}