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
        public void InvalidDeltaTickIntervalsDoFailValidation(double interval)
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
        [TestCase(0.01)]
        [TestCase(5)]
        public void ElapsedTimeIsAccurateGivenValidConstantDeltaIntervals(double interval)
        {
            var _ = new CoreTick(TickSystemConstructionUtility.BlankCoreTickSystemConfigData());
            const int tickCount = 50;
            for (int i = 0; i < tickCount; i++)
            {
                Core.Tick.OnUpdate(interval);
            }
            
            TimeSpan elapsed = TimeSpan.FromSeconds(tickCount * interval);
            Assert.AreEqual(elapsed, Core.Tick.elapsedSinceSimStartup,
                "Elapsed time does not add up based on constant delta intervals.");
        }

        #endregion Tests
    }
}