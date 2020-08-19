using System;
using System.Collections.Generic;
using SharpCore.Ticking.Data;
using SharpCore.Ticking.Validation;

namespace SharpCore.Ticking
{
    //Base from: 
    //https://forum.unity.com/threads/writing-update-manager-what-should-i-know.402571/
    
    /// <summary>
    /// Implementation of Core.ICoreTick interface; coordinates ticking of render/simulation loops.
    /// </summary>
    public class CoreTick : CoreSystemBase<CoreTickSystemConfigData>, ICoreTick
    {
        #region Data

        /// <summary>
        /// List of variable (render) ticks
        /// </summary>
        private readonly ITickInstance<ITickRenderClient> _renderTick;
        
        /// <summary>
        /// List of fixed (simulation) ticks
        /// </summary>
        private readonly TickSimulation[] _simulationTicks;
        
        /// <summary>
        /// How long the simulation has been running.
        /// </summary>
        public TimeSpan elapsedSinceSimStartup { get; private set; }
        
        #endregion Data
        
        
        #region Construction

        public CoreTick(CoreTickSystemConfigData data) : base(data)
        {
            // Make sure we have valid ticking data
            if (!CoreTickValidation.ValidateCoreTickSystemConfigData(data))
                return;
            
            // Create default render tick
            _renderTick = new TickRender(data.renderTicksets);
            
            // Create simulation ticks and add to lists
            List<TickSimulation> fixedList = new List<TickSimulation>();
            foreach (var sim in _systemData.simulationTicks)
            {
                fixedList.Add(new TickSimulation(sim));
            }
            _simulationTicks = fixedList.ToArray();
        }

        #endregion Construction


        #region Registration

        void ICoreTick.Register(ITickSimulationClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickSimulationClient> s = GetSimulationTickset(tickset, _simulationTicks);
            s?.stagedForAddition.Add(obj);
        }
        
        void ICoreTick.Register(ITickRenderClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickRenderClient> s = GetRenderTickset(tickset, _renderTick);
            s?.stagedForAddition.Add(obj);
        }

        void ICoreTick.Unregister(ITickSimulationClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickSimulationClient> s = GetSimulationTickset(tickset, _simulationTicks);
            s?.stagedForRemoval.Add(obj);
        }

        void ICoreTick.Unregister(ITickRenderClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickRenderClient> s = GetRenderTickset(tickset, _renderTick);
            s?.stagedForRemoval.Add(obj);
        }

        #endregion Registration
        

        #region Source

        void ICoreTick.OnUpdate(double delta)
        {
            // Validate delta data
            if (!CoreTickValidation.ValidateDeltaInterval(delta))
                return;
            
            // Update total elapsed time
            elapsedSinceSimStartup += TimeSpan.FromSeconds(delta);
            
            // Tick simulations
            ExecuteSimulationTicks(delta, _simulationTicks);
            ExecuteRenderTicks(delta, _renderTick);
        }

        #endregion Source
        
        
        #region Tick

        /// <summary>
        /// Ticks simulation.
        /// </summary>
        /// <param name="delta">Delta since last tick (seconds).</param>
        /// <param name="simulationTicks">The simulation ticks on which to execute.</param>
        private static void ExecuteSimulationTicks(double delta, IEnumerable<TickSimulation> simulationTicks)
        {
            foreach (TickSimulation sim in simulationTicks)
            {
                double tickrate = sim.configData.tickrate;
                sim.accumulator += delta;
                sim.accumulator = Math.Min(sim.accumulator, sim.configData.maxDelta);

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
        private static void ExecuteRenderTicks(double delta, ITickInstance<ITickRenderClient> renderTick)
        {
            renderTick.Tick(delta);
        }
        
        #endregion Tick
        
        
        #region Tickset Matching

        private static TicksetBaseInstance<ITickSimulationClient> GetDefaultSimulationTickset(
            TickSimulation[] simulationTicks)
        {
            return simulationTicks[0].ticksets[0];
        }

        private static TicksetBaseInstance<ITickRenderClient> GetDefaultRenderTickset(
            ITickInstance<ITickRenderClient> renderTick)
        {
            return renderTick.ticksets[0];
        }

        private static TicksetBaseInstance<ITickSimulationClient> GetSimulationTickset(
            TicksetInstanceConfigData data,
            TickSimulation[] simulationTicks)
        {
            // Check for null
            if (data == null)
            {
                return GetDefaultSimulationTickset(simulationTicks);
            }
            
            // Find tickset
            TicksetBaseInstance<ITickSimulationClient> match = null;
            foreach (TickSimulation t in simulationTicks)
            {
                foreach (TicksetBaseInstance<ITickSimulationClient> ts in t.ticksets)
                {
                    if (ts.ticksetName == data.ticksetName)
                    {
                        match = ts;
                        goto matched;
                    }
                }
            }
            
            matched:
            return match;
        }

        private static TicksetBaseInstance<ITickRenderClient> GetRenderTickset(
            TicksetInstanceConfigData data, 
            ITickInstance<ITickRenderClient> renderTick)
        {
            // Check for null
            if (data == null)
            {
                return GetDefaultRenderTickset(renderTick);
            }
            
            // Find tickset
            TicksetBaseInstance<ITickRenderClient> match = null;
            foreach (TicksetBaseInstance<ITickRenderClient> t in renderTick.ticksets)
            {
                if (t.ticksetName == data.ticksetName)
                { 
                    match = t;
                    break;
                }
            }
            
            return match;
        }
        
        #endregion Tickset Matching
    }
}