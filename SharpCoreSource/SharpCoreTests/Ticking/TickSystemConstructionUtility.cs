using SharpCore.Ticking;

namespace SharpCoreTests.Ticking
{
    public static class TickSystemConstructionUtility
    {
        public static float blankSimTickRate = 0.034f;
        
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

        private static TicksetConfigData[] BlankTicksetGroup()
        {
            TicksetConfigData [] ticksetGroup =
            {
                new TicksetConfigData
                {
                    ticksetName = "tickset"
                }
            };
            return ticksetGroup;
        }

        public static TicksetConfigData TicksetInstance(string name)
        {
            return new TicksetConfigData
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
                        new TicksetConfigData
                        {
                            ticksetName = "testSimTickset"
                        }
                    },
                    maxDelta = 0.5f,
                    tickName = "testSimulationTick",
                    tickrate = blankSimTickRate
                }
            };
            return testSimulationTicks;
        }

        public static TickSimulationConfigData[] SimulationTickDataGroup(
            int tickCount, 
            int ticksetsPerTick,
            float tRate = 0.0334f,
            float tMax = 0.5f)
        {
            TickSimulationConfigData[] data = new TickSimulationConfigData[tickCount];
            for (int i = 0; i < tickCount; i++)
            {
                TickSimulationConfigData thisTick = new TickSimulationConfigData();
                thisTick.tickName = "tick_" + i;
                thisTick.ticksets = new TicksetConfigData[ticksetsPerTick];
                thisTick.tickrate = tRate;
                thisTick.maxDelta = tMax;
                for (int e = 0; e < thisTick.ticksets.Length; e++)
                {
                    thisTick.ticksets[e] = new TicksetConfigData
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