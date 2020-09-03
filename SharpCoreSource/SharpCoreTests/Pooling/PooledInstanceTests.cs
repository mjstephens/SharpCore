using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpCore.Utility.Pooling;
using SharpCoreTests.Pooling.Demo;

namespace SharpCoreTests.Pooling
{
    [TestFixture]
    public class PooledInstanceTests
    {
        [Test]
        [TestCase(10,5)]
        [TestCase(5,10)]
        public static void PoolRemoveFromInstanceDoesFunction(int spawnNum, int removeCount)
        {
            IPool pool = new DemoPool();
            List<DemoPooledObjectInstance> _instances = new List<DemoPooledObjectInstance>();
            
            // Spawn instances
            for (int i = 0; i < spawnNum; i++)
            {
                _instances.Add((DemoPooledObjectInstance)pool.GetNext());
            }
            
            // Delete instances
            for (int i = 0; i < Math.Min(removeCount, _instances.Count); i++)
            {
                _instances[i].DeleteObjectInstance();
            }
            
            Assert.AreEqual(Math.Max(spawnNum - removeCount, 0), pool.instanceCount,
                "Pools are not properly removing instances when the removal command comes from the instance!");
        }

        [Test]
        [TestCase(10, 5)]
        [TestCase(5, 10)]
        public static void PoolsCanRelenquishAndReClaimInstances(int spawnNum, int relenquishCount)
        {
            IPool pool = new DemoPool();
            List<DemoPooledObjectInstance> _instances = new List<DemoPooledObjectInstance>();
            
            // Spawn instances
            for (int i = 0; i < spawnNum; i++)
            {
                _instances.Add((DemoPooledObjectInstance)pool.GetNext());
            }
            
            // Delete instances
            for (int i = 0; i < Math.Min(relenquishCount, _instances.Count); i++)
            {
                _instances[i].RelenquishFromInstance();
            }
            
            Assert.AreEqual(Math.Max(spawnNum - relenquishCount, 0), pool.activeCount,
                "Pooled instances are not being read as relenquished!");
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]        
        [TestCase(20)]
        public static void RelenquishedPoolItemsAreMovedToFrontOfPoolInstanceList(int rand)
        {
            PoolBase pool = new DemoPool();
            List<DemoPooledObjectInstance> _instances = new List<DemoPooledObjectInstance>();
            
            // Spawn instances
            for (int i = 0; i < 200; i++)
            {
                _instances.Add((DemoPooledObjectInstance)pool.GetNext());
            }
            
            Random r = new Random(rand);
            int rCount = 0;
            for (int i = 0; i < _instances.Count; i++)
            {
                if (r.NextDouble() > 0.5)
                {
                    rCount++;
                    _instances[i].RelenquishFromInstance();
                }
            }
            
            Assert.True(pool.InternalValidatePoolInstanceLayout(rCount),
                "Pool is not arranging available instances at front of instance list!");
        }
    }
}