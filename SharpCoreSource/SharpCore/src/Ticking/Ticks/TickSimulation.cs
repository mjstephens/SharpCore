using System.Collections.Generic;

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
        public float accumulator { get; set; }

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
        private void SetTicksets(IEnumerable<TicksetConfigData> ticksetsData)
        {
            ticksets = new List<TicksetBase<ITickSimulationClient>>();
            foreach (TicksetConfigData tick in ticksetsData)
            {
                TicksetSimulation t = new TicksetSimulation(tick);
                ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}