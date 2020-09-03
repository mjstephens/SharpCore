using NUnit.Framework;
using SharpCore.Utility.Pooling;
using SharpCoreTests.Pooling.Demo;

namespace SharpCoreTests.Pooling
{
    [TestFixture]
    public class PooledObjectsCreationTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(50)]
        public static void PoolDoesCreateObjects(int spawnCount)
        { 
            IPool pool = new DemoPool();
            for (int i = 0; i < spawnCount; i++)
            {
                pool.GetNext(); 
            }
            
            Assert.AreEqual(spawnCount, pool.instanceCount,
               "Pool is not spawning instances correctly!");
        }

        [Test]
        [TestCase(-1, 10)]
        [TestCase(5, 10)]
        [TestCase(10, 5)]
        public static void PoolDoesRecycleObjects(int poolMaxCapacity, int spawnCount)
        {
            IPool pool = new DemoPool();
            pool.capacityMax = poolMaxCapacity;
            
            for (int i = 0; i < spawnCount; i++)
            {
                pool.GetNext();
            }

            int equalTarg = spawnCount;
            if (poolMaxCapacity > 0 && poolMaxCapacity < spawnCount)
                equalTarg = poolMaxCapacity;
            
            Assert.AreEqual(equalTarg, pool.instanceCount,
                "Pool is not properly recycling instances!");
        }
    }
}