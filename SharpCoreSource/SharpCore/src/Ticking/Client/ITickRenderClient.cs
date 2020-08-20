namespace SharpCore.Ticking.Client
{
    public interface ITickRenderClient : ITickClient
    {
        void Tick(double delta);
    }
}