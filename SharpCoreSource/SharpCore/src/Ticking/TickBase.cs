using System.Collections.Generic;

namespace SharpCore.Ticking
{
    public abstract class TickBase<T> : ITickInstance<T> where T: ITickClient
    {
        #region Properties
        
        /// <summary>
        /// The ticksets that belong to this tick
        /// </summary>
        public List<TicksetBaseInstance<T>> ticksets { get; protected set; }

        /// <summary>
        /// How many ticks this tick has counted
        /// </summary>
        public uint tickCount { get; private set; }

        /// <summary>
        /// FPS of this tick
        /// </summary>
        public double ticksPerSecond { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private const float CONST_tpsUpdateRate = 4.0f;

        #endregion Properties


        #region Variables

        private int _tpsFrameCount;
        private double _tpsAccumDelta;

        #endregion Variables


        #region Tick
        
        void ITickInstance<T>.Tick(double delta)
        {
            foreach (TicksetBaseInstance<T> sim in ticksets)
            {
                sim.Tick(delta);
            }
            
            CalculateTPS(delta);
            tickCount++;
        }

        /// <summary>
        /// Calculates the approximate realtime frames per second (tps) for this tick.
        /// </summary>
        /// <param name="delta">Time elapsed (seconds) since previous tick.</param>
        private void CalculateTPS(double delta)
        {
            _tpsFrameCount++;
            _tpsAccumDelta += delta;
            if (_tpsAccumDelta > 1.0f / CONST_tpsUpdateRate)
            {
                ticksPerSecond = _tpsFrameCount / _tpsAccumDelta;
                _tpsFrameCount = 0;
                _tpsAccumDelta -= 1.0f / CONST_tpsUpdateRate;
            }
        }
        
        #endregion Tick
    }
}