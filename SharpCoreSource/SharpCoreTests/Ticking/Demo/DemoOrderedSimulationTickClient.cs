using SharpCore;
using SharpCore.Ticking;

namespace SharpCoreTests.Ticking.Demo
{
    public class DemoOrderedSimulationTickClient : ITickSimulationClient
    {
        #region Data

        /// <summary>
        /// We increment this value every time a render tickset is ticked, giving us a view of the tick order
        /// </summary>
        public static int tickOrderCounter;

        public readonly int targetOrder;
        public int thisOrderedEntryResult { get; private set; }

        #endregion Data
        
        
        #region Constructor

        public DemoOrderedSimulationTickClient(
            TicksetConfigData data, 
            int targetOrder)
        {
            this.targetOrder = targetOrder;
            Core.Tick.Register(this, data);
        }

        #endregion Constructor
        
        
        #region Tick

        public void Tick(double delta)
        {
            thisOrderedEntryResult = tickOrderCounter;
            tickOrderCounter++;
        }

        #endregion Tick
    }
}