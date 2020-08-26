namespace SharpCore.Ticking
{
    public class TicksetRender : TicksetBase<ITickRenderClient>
    {
        #region Constructor

        public TicksetRender(TicksetConfigData data)
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
            foreach (ITickRenderClient obj in _current)
            {
                obj.Tick(delta);
            }
        }

        #endregion
    }
}