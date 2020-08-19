using SharpCore.Ticking.Data;

namespace SharpCore.Ticking
{
    public class TicksetSimulationInstance: TicksetBaseInstance<ITickSimulationClient>
    {
        #region Constructor

        public TicksetSimulationInstance(TicksetInstanceConfigData data)
        {
            ticksetData = data;
        }

        #endregion Constructor
        
        
        #region Tick

        /// <summary>
        /// Iterates through and ticks every ITickable assigned to this tickset.
        /// </summary>
        public override void Tick(double delta)
        {
            base.Tick(delta);
            foreach (ITickSimulationClient obj in _current)
            {
                obj.Tick(delta);
            }
        }

        #endregion
    }
}