using SharpCore.Ticking.Client;
using SharpCore.Ticking.Data;
using SharpCore.Ticking.Interfaces;

namespace SharpCore.Ticking.Utility
{
    /// <summary>
    /// Aids in finding ticksets based on data.
    /// </summary>
    public static class TicksetMatchUtility
    {
        #region Simulation Ticksets

        public static TicksetBaseInstance<ITickSimulationClient> GetSimulationTickset(
            TicksetInstanceConfigData data,
            TickSimulation[] simulationTicks)
        {
            // Check for null
            if (data == null)
            {
                return GetDefaultSimulationTickset(simulationTicks);
            }
            
            // Find tickset
            TicksetBaseInstance<ITickSimulationClient> match = null;
            foreach (TickSimulation t in simulationTicks)
            {
                foreach (TicksetBaseInstance<ITickSimulationClient> ts in t.ticksets)
                {
                    if (ts.ticksetName == data.ticksetName)
                    {
                        match = ts;
                        goto matched;
                    }
                }
            }
            
            matched:
            return match;
        }
        
        private static TicksetBaseInstance<ITickSimulationClient> GetDefaultSimulationTickset(
            TickSimulation[] simulationTicks)
        {
            return simulationTicks[0].ticksets[0];
        }

        #endregion Simulation Ticksets


        #region Render Ticksets

        public static TicksetBaseInstance<ITickRenderClient> GetRenderTickset(
            TicksetInstanceConfigData data, 
            ITickInstance<ITickRenderClient> renderTick)
        {
            // Check for null
            if (data == null)
            {
                return GetDefaultRenderTickset(renderTick);
            }
            
            // Find tickset
            TicksetBaseInstance<ITickRenderClient> match = null;
            foreach (TicksetBaseInstance<ITickRenderClient> t in renderTick.ticksets)
            {
                if (t.ticksetName == data.ticksetName)
                { 
                    match = t;
                    break;
                }
            }
            
            return match;
        }
        
        private static TicksetBaseInstance<ITickRenderClient> GetDefaultRenderTickset(
            ITickInstance<ITickRenderClient> renderTick)
        {
            return renderTick.ticksets[0];
        }

        #endregion Render Ticksets
    }
}