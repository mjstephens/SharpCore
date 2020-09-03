using SharpCore.Utility.Pooling;

namespace SharpCoreTests.Pooling.Demo
{
    public class DemoPool : PoolBase
    {
        protected override IClientPoolable CreateNewPoolable()
        {
            DemoPooledObjectInstance obj = new DemoPooledObjectInstance();
            return obj;
        }
    }
}