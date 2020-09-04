using System;
using NUnit.Framework;
using SharpCore;
using SharpCore.Ticking;

namespace SharpCoreTests.Ticking
{
    [TestFixture]
    public class TickSourceTests
    {
        #region Tests

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void InvalidDeltaTickIntervalsDoFailValidation(float interval)
        {
            try
            {
                var _ = new CoreTick(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
                Core.Tick.OnUpdate(interval);
                Assert.Fail("Invalid tick delta intervals are passing validation.");
            }
            catch (ArgumentOutOfRangeException)
            {
                // We good, failure happened as expected.
            }
        }
        
        [Test]
        [TestCase(0.01f)]
        [TestCase(5)]
        public void ElapsedTimeIsAccurateGivenValidConstantDeltaIntervals(float interval)
        {
            var _ = new CoreTick(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
            const int tickCount = 50;
            for (int i = 0; i < tickCount; i++)
            {
                Core.Tick.OnUpdate(interval);
            }
            
            TimeSpan elapsed = TimeSpan.FromSeconds(tickCount * interval);
            float round = Math.Abs((float)Core.Tick.elapsedSinceSimStartup.TotalSeconds - (float)elapsed.TotalSeconds);
            Assert.LessOrEqual(round, 0.0001f,
                "Elapsed time does not add up based on constant delta intervals.");
        }

        #endregion Tests
    }
}