namespace SharpCore.Ticking
{
    /// <summary>
    /// Implement this interface to receive tick updates.
    /// </summary>
    public interface ITickable
    {
        void Tick(double delta);
    }
}