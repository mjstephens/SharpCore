namespace SharpCore.Ticking
{
    public interface ITickSimulationClient : ITickClient
    {
        /// <summary>
        /// Ticks the simulation client.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick for this simulation tickset.</param>
        void Tick(double delta);
    }
}