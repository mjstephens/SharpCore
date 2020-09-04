namespace SharpCore.Ticking
{
    /// <summary>
    /// Provides tick system with ticks from client; Update (Unity) or custom ticks with other system.
    /// </summary>
    public abstract class CoreTickSource
    {
        /// <summary>
        /// Ticks the core ticking system with the provided update interval.
        /// </summary>
        /// <param name="delta">The time elapsed since the previous tick (in seconds).</param>
        protected static void Tick(float delta)
        {
            Core.Tick.OnUpdate(delta);
        }
    }
}