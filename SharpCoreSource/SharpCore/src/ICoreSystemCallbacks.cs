namespace SharpCore
{
    public interface ICoreSystemCallbacks
    {
        /// <summary>
        /// Called when all of the core systems have finished being constructed.
        /// </summary>
        void OnPostCoreSystemsInitialization();
    }
}