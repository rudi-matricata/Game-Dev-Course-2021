using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test
    {
        [Test]
        public void TestSimplePasses()
        {
            HealthBar hb = new HealthBar();
            HeroCollisions hc = new HeroCollisions();

            int i = 0;
            for(; i < 5; i++) {
                hb.Hearts.Push(null);
            }

            while(i > 0) {
                Assert.AreEqual(i--, hb.Hearts.Count);
                hc.OnEnemy();
            }
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
