using System.Collections.Generic;
using SharpCore.Ticking.Client;
using SharpCore.Ticking.Data;

namespace SharpCore.Ticking
{
    public sealed class TickSimulation : TickBase<ITickSimulationClient>
    {
        #region Variables
        
        /// <summary>
        /// 
        /// </summary>
        public readonly TickSimulationConfigData configData;
        
        /// <summary>
        /// Accumulates delta time and processes correct number of sim ticks in AppTick.
        /// </summary>
        public double accumulator { get; set; }

        #endregion Variables


        #region Constructor

        public TickSimulation(TickSimulationConfigData data)
        {
            configData = data;
            SetTicksets(data.ticksets);
        }

        #endregion Constructor
        
        
        #region Ticksets

        /// <summary>
        /// Creates and sets the ticksets to be used in this tick.
        /// </summary>
        /// <param name="ticksetsData">The data from which to create the ticksets.</param>
        private void SetTicksets(IEnumerable<TicksetInstanceConfigData> ticksetsData)
        {
            ticksets = new List<TicksetBaseInstance<ITickSimulationClient>>();
            foreach (TicksetInstanceConfigData tick in ticksetsData)
            {
                TicksetSimulationInstance t = new TicksetSimulationInstance(tick);
                ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}