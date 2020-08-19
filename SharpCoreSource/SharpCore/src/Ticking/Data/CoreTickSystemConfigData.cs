using SharpCore.Data;

namespace SharpCore.Ticking.Data
{
    public class CoreTickSystemConfigData : CoreSystemData
    {
        public TicksetInstanceConfigData[] renderTicksets;
        public TickSimulationConfigData[] simulationTicks;
    }
}