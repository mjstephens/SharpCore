using SharpCore;
using SharpCore.Ticking.Client;
using SharpCore.Ticking.Data;

namespace SharpCoreTests.Ticking.Demo
{
    public class DemoOrderedRenderTickClient : ITickRenderClient
    {
        #region Data

        /// <summary>
        /// We increment this value every time a render tickset is ticked, giving us a view of the tick order
        /// </summary>
        public static int tickOrderCounter;
        
        public int targetOrder { get; }
        public int thisOrderedEntryResult { get; private set; }

        #endregion Data
        
        
        #region Constructor

        public DemoOrderedRenderTickClient(TicksetInstanceConfigData data, int orderedInstance)
        {
            targetOrder = orderedInstance;
            Core.Tick.Register(this, data);
        }

        #endregion Constructor


        #region Tick

        void ITickRenderClient.Tick(double delta)
        {
            thisOrderedEntryResult = tickOrderCounter;
            tickOrderCounter++;
        }

        #endregion Tick
        
    }
}