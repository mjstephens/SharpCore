namespace SharpCore.Ticking.Client
{
    public interface ITickSimulationClient : ITickClient
    {
        void Tick(double delta);
    }
}