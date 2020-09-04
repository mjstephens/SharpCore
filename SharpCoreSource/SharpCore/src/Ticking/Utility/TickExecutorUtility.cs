using System;
using System.Collections.Generic;

namespace SharpCore.Ticking
{
    /// <summary>
    /// Executes ticks across existing tick instances + ticksets.
    /// </summary>
    public static class TickExecutorUtility
    {
        /// <summary>
        /// Ticks simulation.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="simulationTicks">The simulation ticks on which to execute.</param>
        public static void ExecuteSimulationTicks(float delta, IEnumerable<TickSimulation> simulationTicks)
        {
            foreach (TickSimulation sim in simulationTicks)
            {
                float tickrate = sim.configData.tickrate;
                sim.accumulator += delta;
                sim.accumulator = 
                    Math.Min(sim.accumulator, sim.configData.maxDelta);

                while (sim.accumulator >= tickrate)
                {
                    sim.accumulator -= tickrate;
                    ((ITickInstance<ITickSimulationClient>) sim).Tick(tickrate);
                }
            }
        }
        
        /// <summary>
        /// Ticks rendering.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="renderTick">The render tick on which to operate.</param>
        public static void ExecuteRenderTicks(float delta, ITickInstance<ITickRenderClient> renderTick)
        {
            renderTick.Tick(delta);
        }
    }
}