namespace SharpCore.Ticking
{
    public interface ITickRenderClient : ITickClient
    {
        /// <summary>
        /// Ticks the render client.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick for this render tickset.</param>
        void Tick(double delta);
    }
}