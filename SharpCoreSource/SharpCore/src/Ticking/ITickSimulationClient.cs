namespace SharpCore.Ticking
{
    public interface ITickSimulationClient : ITickClient
    {
        void Tick(double delta);
    }
}