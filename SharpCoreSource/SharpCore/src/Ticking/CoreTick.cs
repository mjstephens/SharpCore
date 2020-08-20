using System;
using System.Collections.Generic;
using SharpCore.Ticking.Client;
using SharpCore.Ticking.Data;
using SharpCore.Ticking.Interfaces;
using SharpCore.Ticking.Utility;

namespace SharpCore.Ticking
{
    //Base from: 
    //https://forum.unity.com/threads/writing-update-manager-what-should-i-know.402571/
    
    /// <summary>
    /// Implementation of Core.ICoreTick interface; coordinates ticking of render/simulation loops.
    /// </summary>
    public class CoreTick : CoreSystemBase<CoreTickSystemConfigData>, ICoreTick
    {
        #region Properties

        public ITickInstance<ITickRenderClient> renderTick { get; }
        public TickSimulation[] simulationTicks { get; }
        public TimeSpan elapsedSinceSimStartup { get; private set; }
        
        #endregion Properties
        
        
        #region Construction

        public CoreTick(CoreTickSystemConfigData data) : base(data)
        {
            // Make sure we have valid ticking data
            if (!CoreTickValidationUtility.ValidateCoreTickSystemConfigData(data))
                return;
            
            // Create default render tick
            renderTick = new TickRender(data.renderTicksets);
            
            // Create simulation ticks and add to lists
            List<TickSimulation> fixedList = new List<TickSimulation>();
            foreach (TickSimulationConfigData sim in _systemData.simulationTicks)
            {
                fixedList.Add(new TickSimulation(sim));
            }
            simulationTicks = fixedList.ToArray();
        }

        #endregion Construction


        #region Registration

        void ICoreTick.Register(ITickSimulationClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickSimulationClient> s = 
                TicksetMatchUtility.GetSimulationTickset(tickset, simulationTicks);
            s?.stagedForAddition.Add(obj);
        }
        
        void ICoreTick.Register(ITickRenderClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickRenderClient> s = 
                TicksetMatchUtility.GetRenderTickset(tickset, renderTick);
            s?.stagedForAddition.Add(obj);
        }

        void ICoreTick.Unregister(ITickSimulationClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickSimulationClient> s = 
                TicksetMatchUtility.GetSimulationTickset(tickset, simulationTicks);
            s?.stagedForRemoval.Add(obj);
        }

        void ICoreTick.Unregister(ITickRenderClient obj, TicksetInstanceConfigData tickset)
        {
            TicksetBaseInstance<ITickRenderClient> s = 
                TicksetMatchUtility.GetRenderTickset(tickset, renderTick);
            s?.stagedForRemoval.Add(obj);
        }

        #endregion Registration
        

        #region Source

        void ICoreTick.OnUpdate(double delta)
        {
            // Validate delta data
            if (!CoreTickValidationUtility.ValidateDeltaInterval(delta))
                return;
            
            // Update total elapsed time
            elapsedSinceSimStartup += TimeSpan.FromSeconds(delta);
            
            // Tick simulations
            TickExecutorUtility.ExecuteSimulationTicks(delta, simulationTicks);
            TickExecutorUtility.ExecuteRenderTicks(delta, renderTick);
        }

        #endregion Source
    }
}