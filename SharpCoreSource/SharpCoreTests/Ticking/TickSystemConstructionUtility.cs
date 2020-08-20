using SharpCore.Ticking.Data;

namespace SharpCoreTests.Ticking
{
    public static class TickSystemConstructionUtility
    {
        #region System Config Data

        /// <summary>
        /// Creates a blank set of tick system config data for testing purposes (NOT NULL).
        /// </summary>
        /// <returns></returns>
        public static CoreTickSystemConfigData BlankCoreTickSystemConfigData()
        {
            CoreTickSystemConfigData data = new CoreTickSystemConfigData
            {
                renderTicksets = BlankTicksetGroup(), 
                simulationTicks = BlankSimulationTickDataGroup()
            };
            return data;
        }

        /// <summary>
        /// Returns tick system data with null render tickset values
        /// </summary>
        /// <returns></returns>
        public static CoreTickSystemConfigData TickSystemDataWithNullRenderTicksets()
        {
            CoreTickSystemConfigData data = new CoreTickSystemConfigData
            {
                renderTicksets = null, 
                simulationTicks = BlankSimulationTickDataGroup()
            };
            return data;
        }
        
        /// <summary>
        /// Returns tick system config data with null simulation tick data
        /// </summary>
        /// <returns></returns>
        public static CoreTickSystemConfigData TickSystemDataWithNullSimulationTicks()
        {
            CoreTickSystemConfigData data = new CoreTickSystemConfigData
            {
                renderTicksets = BlankTicksetGroup(), 
                simulationTicks = null
            };
            return data;
        }

        #endregion System Config Data
        

        #region Ticksets

        private static TicksetInstanceConfigData[] BlankTicksetGroup()
        {
            TicksetInstanceConfigData [] ticksetGroup =
            {
                new TicksetInstanceConfigData
                {
                    ticksetName = "tickset"
                }
            };
            return ticksetGroup;
        }

        public static TicksetInstanceConfigData TicksetInstance(string name)
        {
            return new TicksetInstanceConfigData
            {
                ticksetName = name
            };
        }

        #endregion Ticksets

        
        #region Simulation Ticks

        private static TickSimulationConfigData[] BlankSimulationTickDataGroup()
        {
            TickSimulationConfigData[] testSimulationTicks =
            {
                new TickSimulationConfigData
                {
                    ticksets = new []
                    {
                        new TicksetInstanceConfigData
                        {
                            ticksetName = "testSimTickset"
                        }
                    },
                    maxDelta = 0.5f,
                    tickName = "testSimulationTick",
                    tickrate = 0.0334
                }
            };
            return testSimulationTicks;
        }

        public static TickSimulationConfigData[] SimulationTickDataGroup(
            int tickCount, 
            int ticksetsPerTick,
            double tRate = 0.0334,
            double tMax = 0.5)
        {
            TickSimulationConfigData[] data = new TickSimulationConfigData[tickCount];
            for (int i = 0; i < tickCount; i++)
            {
                TickSimulationConfigData thisTick = new TickSimulationConfigData();
                thisTick.tickName = "tick_" + i;
                thisTick.ticksets = new TicksetInstanceConfigData[ticksetsPerTick];
                thisTick.tickrate = tRate;
                thisTick.maxDelta = tMax;
                for (int e = 0; e < thisTick.ticksets.Length; e++)
                {
                    thisTick.ticksets[e] = new TicksetInstanceConfigData
                    {
                        ticksetName = "tick_" + i + "_tickset_" + e
                    };
                }
                data[i] = thisTick;
            }
            return data;
        }

        #endregion Simulation Ticks
    }
}