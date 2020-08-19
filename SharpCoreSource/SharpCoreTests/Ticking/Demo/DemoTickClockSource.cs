using SharpCore.Ticking.Sources;

namespace SharpCoreTests.Ticking.Demo
{
    public class DemoTickClockSource : ICoreTickSource
    {
        /// <summary>
        /// Tests triggering a core system tick with the provided delta.
        /// </summary>
        /// <param name="delta">Time(in seconds) since the previous tick.</param>
        public void TestTick(float delta)
        {
            Tick(delta);
        }
    }
}