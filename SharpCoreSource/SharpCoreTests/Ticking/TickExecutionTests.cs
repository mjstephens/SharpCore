using System.Collections.Generic;
using NUnit.Framework;
using SharpCore;
using SharpCore.Ticking;
using SharpCore.Ticking.Data;
using SharpCoreTests.Ticking.Demo;

namespace SharpCoreTests.Ticking
{
    [TestFixture]
    public class TickExecutionTests
    {
        #region Execution Order
        
        [Test]
        [TestCase(2)]
        [TestCase(5)]
        public void RenderTicksetsAreExecutedInOrderCreated(int ticksetCount)
        {
            // Create tick system with render ticksets
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.renderTicksets = new TicksetInstanceConfigData[ticksetCount];
            for (int i = 0; i < ticksetCount; i++)
            {
                coreTickConfigData.renderTicksets[i] = new TicksetInstanceConfigData
                {
                    ticksetName = i.ToString()
                };
            }
            new CoreTick(coreTickConfigData);
            
            // Create clients in order
            DemoOrderedRenderTickClient.tickOrderCounter = 0;
            DemoOrderedRenderTickClient[] clients = new DemoOrderedRenderTickClient[coreTickConfigData.renderTicksets.Length];
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i] = new DemoOrderedRenderTickClient(coreTickConfigData.renderTicksets[i], i);
            }
            
            // Tick
            Core.Tick.OnUpdate(0.0334);
            
            // Client tick indices should match their tick keys
            foreach (var c in clients)
            {
                Assert.AreEqual(c.targetOrder, c.thisOrderedEntryResult,
                    "Render ticksets are not executing in the order they were created.");
            }
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(5, 0)]
        [TestCase(5, 5)]
        [TestCase(3, 7)]
        public void SimulationTicksetsAreExecutedInOrderCreated(int ticks, int ticksetsPerTick)
        {
            // Tick data
            const double tRate = 0.035;
            const double tMax = 0.05;
            
            // Construct data with additional simulation ticks/ticksets
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.simulationTicks =
                TickSystemConstructionUtility.SimulationTickDataGroup(
                    ticks, 
                    ticksetsPerTick, 
                    tRate, 
                    tMax);
            new CoreTick(coreTickConfigData);
            
            // Create clients in order
            DemoOrderedSimulationTickClient.tickOrderCounter = 0;
            List<DemoOrderedSimulationTickClient> clients = new List<DemoOrderedSimulationTickClient>();
            int count = 0;
            for (int i = 0; i < coreTickConfigData.simulationTicks.Length; i++)
            {
                for (int e = 0; e < coreTickConfigData.simulationTicks[i].ticksets.Length; e++)
                {
                    clients.Add(new DemoOrderedSimulationTickClient(
                        coreTickConfigData.simulationTicks[i].ticksets[e],
                        count));
                    count++;
                }
            }
            
            // Tick; make sure the delta is long enough to tick simulation
            double diff = tMax - tRate;
            Core.Tick.OnUpdate(tMax - (diff / 2));
            
            // Client tick indices should match their tick keys
            foreach (var c in clients)
            {
                Assert.AreEqual(c.targetOrder, c.thisOrderedEntryResult,
                    "Simulation ticksets are not executing in the order they were created.");
            }
        }
        
        #endregion Execution Order
    }
}