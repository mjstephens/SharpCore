using SharpCore.Ticking.Client;

namespace SharpCoreTests.Ticking.Demo
{
    public class DemoSimulationTickClientIntervalsTest : ITickSimulationClient
    {
        #region Properties

        public int ticksSinceLastCheck { get; set; }

        #endregion Properties
        

        #region Tick

        void ITickSimulationClient.Tick(double delta)
        {
            ticksSinceLastCheck++;
        }

        #endregion Tick
    }
}