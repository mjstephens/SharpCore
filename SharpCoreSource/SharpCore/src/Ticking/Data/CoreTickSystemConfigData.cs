using SharpCore.Data;

namespace SharpCore.Ticking
{
    public class CoreTickSystemConfigData : CoreSystemData
    {
        public TicksetConfigData[] renderTicksets;
        public TickSimulationConfigData[] simulationTicks;
    }
}