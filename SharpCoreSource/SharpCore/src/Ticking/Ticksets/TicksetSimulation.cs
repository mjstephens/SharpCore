namespace SharpCore.Ticking
{
    public class TicksetSimulation: TicksetBase<ITickSimulationClient>
    {
        #region Constructor

        public TicksetSimulation(TicksetConfigData data)
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