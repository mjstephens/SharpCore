using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpCore.Utility.Pooling;
using SharpCoreTests.Pooling.Demo;

namespace SharpCoreTests.Pooling
{
    [TestFixture]
    public class PoolCapacityTests
    {
        [Test]
        [TestCase(5, 10)]
        public static void PoolDoesEnforceRecyclingAfterMaxCapacityReached(int poolMaxCapacity, int activationCount)
        {
            IPool pool = new DemoPool();
            pool.capacityMax = poolMaxCapacity;
            for (int i = 0; i < activationCount; i++)
            {
                pool.GetNext(); 
            }
            
            Assert.LessOrEqual(pool.instanceCount, poolMaxCapacity,
                "Pool is failing to recycle after maximum instance capacity is reached!");
        }

        [Test]
        [TestCase(5)]
        public static void PoolDoesDeleteOverflowInstancesAfterMaxCapacityExceeded(int poolMaxCapacity)
        {
            IPool pool = new DemoPool();
            pool.capacityMax = poolMaxCapacity * 2;
            for (int i = 0; i < pool.capacityMax; i++)
            {
                pool.GetNext(); 
            }
            
            // Set pool max to a lower number; this should force the pool to delete objects
            pool.capacityMax = poolMaxCapacity;
            
            Assert.LessOrEqual(pool.instanceCount, poolMaxCapacity,
                "Pool is failing to destroy overflow instances after max capacity is exceeded!");
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        public static void PoolDoesCreateMinimumCapacity(int poolMinCapacity)
        {
            IPool pool = new DemoPool();
            pool.capacityMin = poolMinCapacity;
            
            Assert.GreaterOrEqual(pool.instanceCount, poolMinCapacity,
                "Pool is not creating enough instances to meet required minimum!");
        }

        [Test]
        [TestCase(10, 5)]
        [TestCase(5, 10)]
        public static void PoolCleanDoesRemoveAvailableInstances(int spawnCount, int deactivateCount)
        {
            IPool pool = new DemoPool();
            List<DemoPooledObjectInstance> _instances = new List<DemoPooledObjectInstance>();
            
            // Spawn instances
            for (int i = 0; i < spawnCount; i++)
            {
                _instances.Add((DemoPooledObjectInstance)pool.GetNext());
            }
            
            // Deactivate instances
            for (int i = 0; i < Math.Min(deactivateCount, _instances.Count); i++)
            {
                _instances[i].RelenquishFromInstance();
            }
            
            // Clean pool
            pool.Clean();
            
            Assert.AreEqual(Math.Max(spawnCount - deactivateCount, 0), pool.instanceCount,
                "Pool cleaning does not remove unused instances as expected!");
        }
    }
}