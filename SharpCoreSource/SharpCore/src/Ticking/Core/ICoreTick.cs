using System;

namespace SharpCore.Ticking
{
    /// <summary>
    /// Describes client-facing API for core tick system
    /// </summary>
    public interface ICoreTick : ICoreSystemInterface
    {
        #region Properties

        /// <summary>
        /// The render tick.
        /// </summary>
        public ITickInstance<ITickRenderClient> renderTick { get; }
        /// <summary>
        /// List of fixed (simulation) ticks
        /// </summary>
        public TickSimulation[] simulationTicks { get; }
        
        /// <summary>
        /// The amount of time elapsed since the simulation began.
        /// </summary>
        public TimeSpan elapsedSinceSimStartup { get; }

        #endregion Properties


        #region Methods

        /// <summary>
        /// Registers a client to a simulation tickset.
        /// </summary>
        /// <param name="obj">The client being registered.</param>
        /// <param name="tickset">The tickset to which to register the client.</param>
        void Register(ITickSimulationClient obj, TicksetConfigData tickset = null);
        
        /// <summary>
        /// Registers a client to a render tickset.
        /// </summary>
        /// <param name="obj">The client being registered.</param>
        /// <param name="tickset">The tickset to which to register the client.</param>
        void Register(ITickRenderClient obj, TicksetConfigData tickset = null);
        
        /// <summary>
        /// Unregisters a client from a simulation tickset.
        /// </summary>
        /// <param name="obj">The client being unregistered.</param>
        /// <param name="tickset">The tickset from which to unregister the client.</param>
        void Unregister(ITickSimulationClient obj, TicksetConfigData tickset = null);
        
        /// <summary>
        /// Unregisters a client from a render tickset.
        /// </summary>
        /// <param name="obj">The client being unregistered.</param>
        /// <param name="tickset">The tickset from which to unregister the client.</param>
        void Unregister(ITickRenderClient obj, TicksetConfigData tickset = null);

        /// <summary>
        /// Ticks the core tick system with the given delta time.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous tick.</param>
        void OnUpdate(double delta);

        #endregion Methods
    }
}