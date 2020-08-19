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

        #endregion Ticksets
        

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
    }
}