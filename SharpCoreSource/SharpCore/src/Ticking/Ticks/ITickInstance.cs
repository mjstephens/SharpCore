using System.Collections.Generic;

namespace SharpCore.Ticking
{
    /// <summary>
    /// Defines contract for an instance of a TickBase (TickRender or TickSimulation) 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITickInstance<T> where T : ITickClient
    {
        #region Properties

        /// <summary>
        /// The ticksets that belong to this tick
        /// </summary>
        public List<TicksetBase<T>> ticksets { get; }

        /// <summary>
        /// How many ticks have been executed thus far.
        /// </summary>
        public uint tickCount { get; }
        
        /// <summary>
        /// FPS of this tick
        /// </summary>
        public float ticksPerSecond { get; }

        #endregion Properties


        #region Methods

        /// <summary>
        /// Executes a tick of the tick instance (ticks every tickset).
        /// </summary>
        /// <param name="delta">Time elapsed (seconds) since previous tick.</param>
        void Tick(float delta);
        
        #endregion Methods
    }
}