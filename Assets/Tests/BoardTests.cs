using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

    public class BoardTests
    {
        TestSetup _setup = new TestSetup();

        [Test]
        public void AtLeastOnePaddleIsSuccesfullyCreated()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Assert.IsNotNull(paddles);
        }
        [Test]
        public void TwoPaddlesAreSuccesfullyCreated()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            //assert if number of paddles does not equal 2
            Assert.AreEqual(2, paddles.Length);
        }

        [Test]
        public void CreatedPaddlesHaveCorrectNames()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            for (int i=0; i<paddles.Length; i++)
            {
                Assert.AreEqual("Paddle" + (i+1).ToString(), paddles[i].gameObject.name);
            }
        }

        [Test]
        public void Paddle1HasCorrectName()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Assert.AreEqual("Paddle1", paddles[0].gameObject.name);
        }

        [Test]
        public void Paddle2HasCorrectName()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Assert.AreEqual("Paddle2", paddles[1].gameObject.name);
        }

        [Test]
        public void Paddle1HasCorrectPosition()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Assert.AreEqual(new Vector3(-6, 0, 0), paddles[0].gameObject.transform.position);
        }

        [Test]
        public void Paddle2HasCorrectPosition()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Assert.AreEqual(new Vector3(6, 0, 0), paddles[1].gameObject.transform.position);
        }

        [Test]
        public void BallIsSuccesfullyCreated()
        {
            BoardManager board = new BoardManager();
            GameObject ball = board.CreateBall();

            Assert.IsNotNull(ball);
        }

        [UnityTest]
        public IEnumerator Paddle1StaysInUpperCameraBounds()
        {
            Camera cam = _setup.CreateCameraForTest();

            GameObject[] paddles = _setup.CreatePaddlesForTest();

            float time = 0;
            while (time < 5)
            {
                paddles[0].GetComponent<Paddle>().RenderPaddle();
                paddles[0].GetComponent<Paddle>().MoveUpY("Paddle1");
                time += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();

            Assert.LessOrEqual(paddles[0].transform.position.y, 4.15);

            //edge of paddle should not leave edge of screen
        }

        [UnityTest]
        public IEnumerator Paddle1StaysInLowerCameraBounds()
        {
            Camera cam = _setup.CreateCameraForTest();

            GameObject[] paddles = _setup.CreatePaddlesForTest();

            float time = 0;
            while (time < 5)
            {
                paddles[0].GetComponent<Paddle>().RenderPaddle();
                paddles[0].GetComponent<Paddle>().MoveDownY("Paddle1");
                time += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();

            // 5 is Camera.main.othographicsize
            Assert.GreaterOrEqual(paddles[0].transform.position.y, -4.15);
        }

        [UnityTest]
        public IEnumerator Paddle2StaysInUpperCameraBounds()
        {
            Camera cam = _setup.CreateCameraForTest();

            GameObject[] paddles = _setup.CreatePaddlesForTest();

            float time = 0;
            while (time < 5)
            {
                paddles[1].GetComponent<Paddle>().RenderPaddle();
                paddles[1].GetComponent<Paddle>().MoveUpY("Paddle2");
                time += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();

            // 4 is (Camera.main.orthographicSize - transform.localScale.y /2)
            Assert.LessOrEqual(paddles[1].transform.position.y, 4.15);

            //edge of paddle should not leave edge of screen
        }

        [UnityTest]
        public IEnumerator Paddle2StaysInLowerCameraBounds()
        {
            Camera cam = _setup.CreateCameraForTest();

            GameObject[] paddles = _setup.CreatePaddlesForTest();
            

            float time = 0;
            while (time < 5)
            {
                paddles[1].GetComponent<Paddle>().RenderPaddle();
                paddles[1].GetComponent<Paddle>().MoveDownY("Paddle2");
                time += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();

            // 4 is (Camera.main.orthographicSize - transform.localScale.y /2)
            Assert.GreaterOrEqual(paddles[1].transform.position.y, -4.15);
        }

        [UnityTest]
        public IEnumerator BallBouncesFromBoardBounds()
        {
            _setup.DestroyAll();
            GameObject ball = _setup.CreateBallForTest();
            ball.GetComponent<Ball>().SetSpeed(0,1);
            Camera cam = _setup.CreateCameraForTest();
            float time = 0;
            while (time < 5)
            {
                ball.GetComponent<Ball>().Move();
                time += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
            Assert.Less(ball.transform.position.y,5);
        }

        [UnityTest]
        public IEnumerator BallCollidesWithPaddle1()
        {
            _setup.DestroyAll();
            _setup.CreatePaddlesForTest();
            GameObject ball = _setup.CreateBallForTest();
            ball.GetComponent<Ball>().SetSpeed(-1,0);
            Camera cam = _setup.CreateCameraForTest();
            float time = 0;
            while (time < 8)
            {
                ball.GetComponent<Ball>().Move();
                time += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
            Assert.Greater(ball.transform.position.x,-7);
        }

        [UnityTest]
        public IEnumerator BallCollidesWithPaddle2()
        {
            _setup.DestroyAll();
            BoardManager board = new BoardManager();
            _setup.CreatePaddlesForTest();
            GameObject ball = board.CreateBall();
            ball.GetComponent<Ball>().SetSpeed(1,0);
            Camera cam = _setup.CreateCameraForTest();
            float time = 0;
            while (time < 8)
            {
                ball.GetComponent<Ball>().Move();
                time += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
            Assert.Less(ball.transform.position.x,7);
        }

        [Test]
        public void ScoreIsInitiallyZeroed()
        {
            BoardManager board = new BoardManager();
            Assert.AreEqual(0,board.playerOneScore);
            Assert.AreEqual(0,board.playerTwoScore);
        }

        [Test]
        public void PlayerOneCanScore()
        {
            _setup.DestroyAll();
            BoardManager board = new BoardManager();
            GameObject ball = board.CreateBall();
            ball.GetComponent<Ball>().SetSpeed(0,0);
            ball.transform.position = new Vector2 (0,7);
            board.CheckForScore();
            Assert.AreEqual(1,board.playerOneScore);
        }
        [Test]
        public void PlayerTwoCanScore()
        {
            _setup.DestroyAll();
            BoardManager board = new BoardManager();
            GameObject ball = board.CreateBall();
            ball.GetComponent<Ball>().SetSpeed(0,0);
            ball.transform.position = new Vector2 (0,-7);
            board.CheckForScore();
            Assert.AreEqual(1,board.playerTwoScore);
        }

        [Test]
        public void BallIsResetAfterScoring()
        {
            _setup.DestroyAll();
            BoardManager board = new BoardManager();
            GameObject ball = board.CreateBall();
            ball.GetComponent<Ball>().SetSpeed(0,0);
            ball.transform.position = new Vector2 (0,-7);
            board.CheckForScore();
            Assert.AreEqual(new Vector3(0,0,0),ball.transform.position);
        }

    }
