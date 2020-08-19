namespace SharpCore.Ticking
{
    public interface ITickRenderClient : ITickClient
    {
        void Tick(double delta);
    }
}