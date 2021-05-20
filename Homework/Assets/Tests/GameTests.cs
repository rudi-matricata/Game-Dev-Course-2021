using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameTests
    {
        [Test]
        public void TestSimplePasses()
        {
            HealthBar healthBar = new HealthBar();
            HeroCollisions heroCollisions = new HeroCollisions();

            int i = 0;
            for(; i < 5; i++) {
                healthBar.Hearts.Push(null);
            }

            while(i > 0) {
                Assert.AreEqual(i--, healthBar.Hearts.Count);
                heroCollisions.OnEnemy();
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
