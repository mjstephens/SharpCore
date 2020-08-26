using System.Collections.Generic;

namespace SharpCore.Ticking
{
    public class TickRender : TickBase<ITickRenderClient>
    {
        #region Constructor

        public TickRender(TicksetConfigData[] data)
        {
            // Add default tickset first
            List<TicksetConfigData> newData = new List<TicksetConfigData>
            {
                new TicksetConfigData {ticksetName = "default"}
            };

            if (data != null)
            {
                newData.AddRange(data);
            }
            
            SetTicksets(newData.ToArray());
        }

        #endregion Constructor


        #region Ticksets

        /// <summary>
        /// Creates and sets the ticksets to be used in this tick.
        /// </summary>
        /// <param name="ticksetsData">The data from which to create the ticksets.</param>
        private void SetTicksets(IEnumerable<TicksetConfigData> ticksetsData)
        {
            ticksets = new List<TicksetBase<ITickRenderClient>>();
            foreach (TicksetConfigData tick in ticksetsData)
            {
                TicksetRender t = new TicksetRender(tick);
                ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}