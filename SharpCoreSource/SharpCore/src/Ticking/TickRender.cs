using System.Collections.Generic;
using SharpCore.Ticking.Client;
using SharpCore.Ticking.Data;

namespace SharpCore.Ticking
{
    public class TickRender : TickBase<ITickRenderClient>
    {
        #region Constructor

        public TickRender(TicksetInstanceConfigData[] data)
        {
            // Add default tickset first
            List<TicksetInstanceConfigData> newData = new List<TicksetInstanceConfigData>
            {
                new TicksetInstanceConfigData {ticksetName = "default"}
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
        private void SetTicksets(IEnumerable<TicksetInstanceConfigData> ticksetsData)
        {
            ticksets = new List<TicksetBaseInstance<ITickRenderClient>>();
            foreach (TicksetInstanceConfigData tick in ticksetsData)
            {
                TicksetRenderInstance t = new TicksetRenderInstance(tick);
                ticksets.Add(t);
            }
        }

        #endregion Ticksets
    }
}