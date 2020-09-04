using System.Collections.Generic;

namespace SharpCore.Ticking
{
    public abstract class TickBase<T> : ITickInstance<T> where T: ITickClient
    {
        #region Properties
        
        public List<TicksetBase<T>> ticksets { get; protected set; }
        public uint tickCount { get; private set; }
        public float ticksPerSecond { get; private set; }

        #endregion Properties


        #region Variables

        private const float CONST_tpsUpdateRate = 4.0f;
        private int _tpsFrameCount;
        private float _tpsAccumDelta;

        #endregion Variables


        #region Tick
        
        void ITickInstance<T>.Tick(float delta)
        {
            foreach (TicksetBase<T> sim in ticksets)
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
        private void CalculateTPS(float delta)
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