using SharpCore.Data;

namespace SharpCore
{
    /// <summary>
    /// Base class for core systems; ensures proper loading after creation.
    /// </summary>
    /// <typeparam name="T">CoreSystemData type</typeparam>
    public class CoreSystemBase <T> : ICoreSystemCallbacks where T : CoreSystemData
    {
        #region Variables

        /// <summary>
        /// The configuration data for this system.
        /// </summary>
        protected readonly T _systemData;

        #endregion Variables


        #region Constructor

        /// <summary>
        /// Constructs the system with the system data.
        /// </summary>
        /// <param name="data">The system configuration data.</param>
        protected CoreSystemBase(T data)
        {
            _systemData = data;
            this.AssignCoreInterface();
        }

        #endregion Constructor


        #region Callbacks

        public virtual void OnPostCoreSystemsInitialization()
        {
            
        }

        #endregion Callbacks
    }
}