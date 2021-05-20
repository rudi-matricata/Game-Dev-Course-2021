using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class GameTests
    {
        [Test]
        public void TestLoseHeartWhenHitByEnemy() {
            HealthBar healthBar = new HealthBar();
            HeroCollisions heroCollisions = new HeroCollisions();

            int i = 0;
            for(; i < 5; i++) {
                healthBar.Hearts.Push(null);
            }

            while(i > 0) {
                Assert.AreEqual(healthBar.Hearts.Count, i--);
                heroCollisions.OnEnemy();
            }
        }

        [Test]
        public void TestJump() {
            GameObject testObj = new GameObject("testGO");
            testObj.AddComponent<Rigidbody2D>();
            testObj.AddComponent<Animator>();
            HeroMovement heroMovement = testObj.AddComponent(typeof(HeroMovement)) as HeroMovement;

            Rigidbody2D rb2d = heroMovement.gameObject.GetComponent<Rigidbody2D>();
            Assert.AreEqual(rb2d.velocity.y, 0);

            heroMovement.Start();
            heroMovement.OnJump();

            Assert.Greater(rb2d.velocity.y, 0);
        }
    }
}
