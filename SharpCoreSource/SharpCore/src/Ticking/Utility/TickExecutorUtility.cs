using System;
using System.Collections.Generic;
using SharpCore.Ticking.Client;
using SharpCore.Ticking.Interfaces;

namespace SharpCore.Ticking.Utility
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
        public static void ExecuteSimulationTicks(double delta, TickSimulation[] simulationTicks)
        {
            for (int i = 0; i < simulationTicks.Length; i++)
            //foreach (TickSimulation sim in simulationTicks)
            {
                double tickrate = simulationTicks[i].configData.tickrate;
                simulationTicks[i].accumulator += delta;
                simulationTicks[i].accumulator = 
                    Math.Min(simulationTicks[i].accumulator, simulationTicks[i].configData.maxDelta);

                while (simulationTicks[i].accumulator >= tickrate)
                {
                    simulationTicks[i].accumulator -= tickrate;
                    ((ITickInstance<ITickSimulationClient>) simulationTicks[i]).Tick(tickrate);
                }
            }
        }
        
        /// <summary>
        /// Ticks rendering.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="renderTick">The render tick on which to operate.</param>
        public static void ExecuteRenderTicks(double delta, ITickInstance<ITickRenderClient> renderTick)
        {
            renderTick.Tick(delta);
        }
    }
}