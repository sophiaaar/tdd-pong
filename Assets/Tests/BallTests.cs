using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

    public class BallTests
    {
        TestSetup _setup = new TestSetup();

        [Test]
        public void ThereCanBeOnlyOneBall()
        {
            _setup.DestroyAll();
            BoardManager board = new BoardManager();
            GameObject ball = board.CreateBall();
            GameObject secondBall = board.CreateBall();

            Assert.IsNull(secondBall);
        }

        [UnityTest]
        public IEnumerator BallCanMoveIn2d()
        {
            _setup.DestroyAll();
            GameObject ball = _setup.CreateBallForTest();
            _setup.CreateCameraForTest();
            Vector2 original = new Vector2(ball.transform.position.x, ball.transform.position.y);
            ball.GetComponent<Ball>().SetSpeed(1,-1);
            yield return null;
            Vector2 current = new Vector2(ball.transform.position.x,ball.transform.position.y);
            Assert.AreNotEqual(original, current);
        }
        [UnityTest]
        public IEnumerator BallDoesNotMoveOnZAxis()
        {
            _setup.DestroyAll();
            GameObject ball = _setup.CreateBallForTest();
            _setup.CreateCameraForTest();
            yield return new WaitForFixedUpdate();
            Assert.True(ball.transform.position.z == 0);
        }

        [Test]
        public void BallHasNonZeroSpeedWhenReset()
        {
            GameObject ball = _setup.CreateBallForTest();
            var b = ball.GetComponent<Ball>();
            b.ResetBall();
            var s = b.GetSpeed();
            Assert.AreNotEqual(0, s[0], "The ball was served with no velocity.");
        }
        
        [Test]
        public void BallCanBeServedLeft()
        {
            GameObject ball = _setup.CreateBallForTest();
            var b = ball.GetComponent<Ball>();
            b.ResetBall(Ball.serviceDirection.left);
            var s = b.GetSpeed();
            Assert.Less(s[0],0);
        }

        [Test]
        public void BallCanBeServedRight()
        {
            GameObject ball = _setup.CreateBallForTest();
            var b = ball.GetComponent<Ball>();
            b.ResetBall(Ball.serviceDirection.right);
            var s = b.GetSpeed();
            Assert.Greater(s[0],0);
        }

        [Test]
        public void BallHasCollider()
        {
            GameObject ball = _setup.CreateBallForTest();
            Assert.IsNotNull(ball.GetComponent<BoxCollider2D>());
        }

        [Test]
        public void BallHasRenderer()
        {
            GameObject ball = _setup.CreateBallForTest();
            Assert.IsNotNull(ball.GetComponent<SpriteRenderer>());
        }

        [Test]
        public void BallHasRigidbody()
        {
            GameObject ball = _setup.CreateBallForTest();
            Assert.IsNotNull(ball.GetComponent<Rigidbody2D>());
        }

        [Test]
        public void BallIsKinematic()
        {
            GameObject ball = _setup.CreateBallForTest();
            Assert.IsTrue(ball.GetComponent<Rigidbody2D>().isKinematic);
        }

        [Test]
        public void BallIsSmallEnough()
        {
            GameObject ball = _setup.CreateBallForTest();
            Assert.AreEqual(new Vector3(0.5f,0.5f,0F), ball.transform.localScale);
        }

        [Test]
        public void BallYDirectionInvertOnBounce()
        {
            GameObject ball = _setup.CreateBallForTest();
            var b = ball.GetComponent<Ball>();
            b.SetSpeed(1,-1);
            b.Bounce();
            Assert.AreEqual(new Vector2(1,1), b.GetSpeed());
        }

        [Test]
        public void BallXDirectionInvertsOnHit()
        {
            GameObject ball = _setup.CreateBallForTest();
            var b = ball.GetComponent<Ball>();
            b.SetSpeed(-1,1);
            b.Hit();
            Assert.AreEqual(new Vector2(1,1), b.GetSpeed());
        }

        [Test]
        public void BallYDirectionIsAffectedByHit()
        {
            GameObject ball = _setup.CreateBallForTest();
            var b = ball.GetComponent<Ball>();
            b.SetSpeed(-1,1);
            b.Hit(0.1f);
            Assert.AreEqual(1.1f, b.GetSpeed()[1] );
        }
        
    }
