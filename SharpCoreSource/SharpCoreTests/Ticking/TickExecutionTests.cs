using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpCore;
using SharpCore.Ticking;
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
            coreTickConfigData.renderTicksets = new TicksetConfigData[ticksetCount];
            for (int i = 0; i < ticksetCount; i++)
            {
                coreTickConfigData.renderTicksets[i] = new TicksetConfigData
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
            Core.Tick.OnUpdate(0.0334f);
            
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
            const float tRate = 0.035f;
            const float tMax = 0.05f;
            
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
            float diff = tMax - tRate;
            Core.Tick.OnUpdate(tMax - (diff / 2));
            
            // Client tick indices should match their tick keys
            foreach (var c in clients)
            {
                Assert.AreEqual(c.targetOrder, c.thisOrderedEntryResult,
                    "Simulation ticksets are not executing in the order they were created.");
            }
        }
        
        #endregion Execution Order


        #region Simulation Ticks

        [Test]
        [TestCase(0.0334f)]
        [TestCase(0.05f)]
        [TestCase(1)]
        public void SimulationTicksDoFireAtCorrectIntervalsRelativeToRenderTick(float mockTickrate)
        {
            // Create new tick core system, override with custom simulation tick
            CoreTickSystemConfigData coreTickConfigData = TickSystemConstructionUtility.BlankCoreTickSystemConfigData();
            coreTickConfigData.simulationTicks =
                TickSystemConstructionUtility.SimulationTickDataGroup(
                    1, 
                    1, 
                    mockTickrate,
                    float.MaxValue);
            var _ = new CoreTick(coreTickConfigData);

            // Create simulation tick instance
            DemoSimulationTickClientIntervalsTest simTick = new DemoSimulationTickClientIntervalsTest();
            Core.Tick.Register(simTick);

            Random r = new Random();
            float elapsedSinceLastTick = 0;
            for (int i = 0; i < 100; i++)
            {
                // Tick
                float mockDelta = (float)r.NextDouble();
                Core.Tick.OnUpdate(mockDelta);
                elapsedSinceLastTick += mockDelta;
                
                // Collect target ticks
                int targetTicks = 0;
                float mockSimAccumulator = elapsedSinceLastTick;
                while (mockSimAccumulator >= mockTickrate)
                {
                    mockSimAccumulator -= mockTickrate;
                    targetTicks++;
                }
                elapsedSinceLastTick = mockSimAccumulator;
                
                // Detect ticks
                if (simTick.ticksSinceLastCheck < targetTicks)
                {
                    Assert.Fail(
                        "Simulation tick client is not ticking enough based on render tickrate: "
                        + "TARGET: " + targetTicks
                        + " RESULT: " + simTick.ticksSinceLastCheck);
                }
                else if (simTick.ticksSinceLastCheck > targetTicks)
                {
                    Assert.Fail(
                        "Simulation tick client is ticking too frequently based on render tickrate: "
                        + "TARGET: " + targetTicks
                        + " RESULT: " + simTick.ticksSinceLastCheck);
                }
                else
                {
                    simTick.ticksSinceLastCheck = 0;
                }
            }
        }

        #endregion Simulation Ticks
    }
}