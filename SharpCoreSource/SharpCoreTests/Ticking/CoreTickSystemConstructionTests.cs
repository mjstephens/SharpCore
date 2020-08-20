using System;
using NUnit.Framework;
using SharpCore;
using SharpCore.Ticking;
using SharpCore.Ticking.Data;

namespace SharpCoreTests.Ticking
{
    [TestFixture]
    public class CoreTickSystemConstructionTests
    {
        #region Null Checks

        [Test]
        public void CoreTickSystemNullConfigDataDoesFailValidation()
        {
            try
            {
                var _ = new CoreTick(null);
                Assert.Fail("Tick system with null config data is passing validation.");
            }
            catch (NullReferenceException)
            {
                // We good, failure happened as expected.
            }
        }
        
        [Test]
        public void CoreTickSystemConfigDataWithNullRenderTicksetsDoesFailValidation()
        {
            try
            {
                var _ = new CoreTick(TickSystemConstructionUtility.TickSystemDataWithNullRenderTicksets());
                Assert.Fail("Tick system with null render tickset data is passing validation.");
            }
            catch (NullReferenceException)
            {
                // We good, failure happened as expected.
            }
        }
        
        [Test]
        public void CoreTickSystemConfigDataWithNullSimulationTicksDoesFailValidation()
        {
            try
            {
                var _ = new CoreTick(TickSystemConstructionUtility.TickSystemDataWithNullSimulationTicks());
                Assert.Fail("Tick system with null simulation ticks data is passing validation.");
            }
            catch (NullReferenceException)
            {
                // We good, failure happened as expected.
            }
        }

        #endregion Null Checks


        #region Data Integrity

        [Test]
        [TestCase(0)]
        [TestCase(50)]
        public void RenderTickDoesInitializeWithCorrectNumberOfTicksets(int additionalTicksets)
        {
            // Construct data with additoinal render ticksets
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.renderTicksets = new TicksetInstanceConfigData[additionalTicksets];
            for (int i = 0; i < additionalTicksets; i++)
            {
                coreTickConfigData.renderTicksets[i] = new TicksetInstanceConfigData
                {
                    ticksetName = "testTick_" + i
                };
            }
            var _ = new CoreTick(coreTickConfigData);
            
            // Total render ticksets should be equal to addtional ticksets plus 1 (the default tickset)
            Assert.AreEqual(additionalTicksets + 1, Core.Tick.renderTick.ticksets.Count,
                "Render tickset count is not correct!");
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 5)]
        [TestCase(5, 0)]
        [TestCase(5, 5)]
        public void SimulationTicksDoInitializeWithCorrectNumberOfTicksets(int ticks, int ticksetsPerTick)
        {
            // Construct data with additional simulation ticks/ticksets
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.simulationTicks =
                TickSystemConstructionUtility.SimulationTickDataGroup(ticks, ticksetsPerTick);
            var _ = new CoreTick(coreTickConfigData);

            // Total simulation ticksets should be equal to ticks + ticksets per (simulation ticks have no defaults)
            int count = 0;
            foreach (var tick in Core.Tick.simulationTicks)
            {
                count += tick.ticksets.Count;
            }
            Assert.AreEqual(ticks * ticksetsPerTick, count,
                "Simulation tickset count is not correct!");
        }

        #endregion Data Integrity
    }
}