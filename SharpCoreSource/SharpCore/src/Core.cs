using System.Collections.Generic;
using SharpCore.Ticking;

namespace SharpCore
{
    public static class Core
    {
        #region Core Interfaces

        public static ICoreTick Tick;
        
        #endregion Core Interfaces
        
        
        #region Variables
        
        /// <summary>
        /// List of all the registered app systems.
        /// </summary>
        private static readonly List<ICoreSystemCallbacks> _systems = new List<ICoreSystemCallbacks>();

        #endregion Variables
        
        
        #region Initialization

        /// <summary>
        /// Callback to all systems after all have been constructed/initialized.
        /// </summary>
        public static void OnApplicationClientSystemLoad()
        {
            // Load default system data for any un-initialized systems
            
            // TODO: create & initialize default data for systems, apply here for any that are null
            
            // Inform the systems we have finished loading
            foreach (var system in _systems)
            {
                system.OnPostCoreSystemsInitialization();
            }
        }

        #endregion Initialization
        
        
        #region Assignment

        /// <summary>
        /// Every app system independently assigns themselves to a root interface.
        /// </summary>
        /// <param name="system"></param>
        public static void AssignCoreInterface<T>(this T system)
        {
            switch (system)
            {
                case ICoreTick s:
                    Tick = s;
                    break;
            }

            // Add to systems list
            _systems.Add(system as ICoreSystemCallbacks);
        }
        
        #endregion Assignment
    }
}