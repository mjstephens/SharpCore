using System;
using NUnit.Framework;
using SharpCore.Ticking;

namespace SharpCoreTests.Ticking
{
    [TestFixture]
    public class CoreTickSystemConstructionTests
    {
        #region Null Checks

        [Test]
        public void NullConfigDataDoesFailCoreTickSystemValidation()
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
        public void NullRenderTicksetConfigDataDoesFailCoreTickSystemValidation()
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
        public void NullSimulationTicksConfigDataDoesFailCoreTickSystemValidation()
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

        

        #endregion Data Integrity
    }
}