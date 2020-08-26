namespace SharpCore.Ticking
{
    /// <summary>
    /// Aids in finding ticksets based on data.
    /// </summary>
    public static class TicksetMatchUtility
    {
        #region Simulation Ticksets

        public static TicksetBase<ITickSimulationClient> GetSimulationTickset(
            TicksetConfigData data,
            TickSimulation[] simulationTicks)
        {
            // Check for null
            if (data == null)
            {
                return GetDefaultSimulationTickset(simulationTicks);
            }
            
            // Find tickset
            TicksetBase<ITickSimulationClient> match = null;
            foreach (TickSimulation t in simulationTicks)
            {
                foreach (TicksetBase<ITickSimulationClient> ts in t.ticksets)
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
        
        private static TicksetBase<ITickSimulationClient> GetDefaultSimulationTickset(
            TickSimulation[] simulationTicks)
        {
            return simulationTicks[0].ticksets[0];
        }

        #endregion Simulation Ticksets


        #region Render Ticksets

        public static TicksetBase<ITickRenderClient> GetRenderTickset(
            TicksetConfigData data, 
            ITickInstance<ITickRenderClient> renderTick)
        {
            // Check for null
            if (data == null)
            {
                return GetDefaultRenderTickset(renderTick);
            }
            
            // Find tickset
            TicksetBase<ITickRenderClient> match = null;
            foreach (TicksetBase<ITickRenderClient> t in renderTick.ticksets)
            {
                if (t.ticksetName == data.ticksetName)
                { 
                    match = t;
                    break;
                }
            }
            
            return match;
        }
        
        private static TicksetBase<ITickRenderClient> GetDefaultRenderTickset(
            ITickInstance<ITickRenderClient> renderTick)
        {
            return renderTick.ticksets[0];
        }

        #endregion Render Ticksets
    }
}