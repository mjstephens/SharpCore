using SharpCore;
using SharpCore.Ticking;

namespace SharpCoreTests.Ticking.Demo
{
    public class DemoRenderTickClientInstanceRegistrationTest : ITickRenderClient
    {
        #region Registration

        /// <summary>
        /// Registers this client with the fallback render tickset
        /// </summary>
        public void RegisterTickClient()
        {
            Core.Tick.Register(this);
        }

        /// <summary>
        /// Unregisters this client from the fallback render tickset
        /// </summary>
        public void UnregisterTickClient()
        {
            Core.Tick.Unregister(this);
        }

        #endregion Registration
        
        
        #region Tick

        void ITickRenderClient.Tick(double delta)
        {
            
        }

        #endregion Tick
    }
}