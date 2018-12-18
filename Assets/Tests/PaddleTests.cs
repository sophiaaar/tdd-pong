using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;


public class PaddleTests
    {
        TestSetup _setup = new TestSetup();

        [Test]
        //[PrebuildSetup(typeof(PaddleTests))]
        public void PaddlesHaveSpriteRenderer()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();
            
            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().RenderPaddle();
                Assert.IsNotNull(paddles[i].gameObject.GetComponent<SpriteRenderer>(), "Paddle" + i + " has no SpriteRenderer");
            }
        }

        [Test]
        public void PaddlesHaveCorrectTag()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();
            
            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().SetTagOnPaddle();
                Assert.AreEqual("Paddle", paddles[i].gameObject.tag);
            }
        }

        [Test]
        public void PaddleIsCorrectShapeInY()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().RenderPaddle();
                Assert.AreEqual(2f, paddles[i].gameObject.transform.localScale.y);
            }
        }

        [Test]
        public void PaddleIsCorrectShapeInX()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().RenderPaddle();
                Assert.AreEqual(0.5f, paddles[i].gameObject.transform.localScale.x);
            }
        }

        [Test]
        public void PaddlesHaveBoxCollider2D()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().AddCollider();
                Assert.IsNotNull(paddles[i].gameObject.GetComponent<BoxCollider2D>(), "Paddle" + i + " has no BoxCollider2D");
            }
        }

        [Test]
        public void PaddleHasRigidbody()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();
            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().AddRigidbody();
                Assert.IsNotNull(paddles[i].GetComponent<Rigidbody2D>());
            }
        }

        [Test]
        public void PaddleIsKinematic()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();
            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().AddRigidbody();
                Assert.IsTrue(paddles[i].GetComponent<Rigidbody2D>().isKinematic);
            }
        }

        [Test]
        public void PaddlesHaveCorrectSprite()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            for (int i =0; i<paddles.Length; i++)
            {
                paddles[i].GetComponent<Paddle>().RenderPaddle();
                Assert.AreEqual(AssetDatabase.LoadAssetAtPath("Assets/Sprites/whitesquare.png", typeof(Sprite)) ,paddles[i].gameObject.GetComponent<SpriteRenderer>().sprite);
            }
        }

        [UnityTest]
        public IEnumerator Paddle1CanMoveUpY()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[0].GetComponent<Paddle>().MoveUpY("Paddle1");
            yield return new WaitForFixedUpdate();
            
            Assert.AreNotEqual(0f, paddles[0].transform.position.y);
        }

        [UnityTest]
        public IEnumerator Paddle1CannotMoveUpWhenArrowsArePressed()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[0].GetComponent<Paddle>().MoveUpY("Paddle2");
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(0f, paddles[0].transform.position.y);
        }

        [UnityTest]
        public IEnumerator Paddle2CanMoveUpY()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[1].GetComponent<Paddle>().MoveUpY("Paddle2");
            yield return new WaitForFixedUpdate();

            Assert.AreNotEqual(0f, paddles[1].transform.position.y);
        }

        [UnityTest]
        public IEnumerator Paddle2CannotMoveUpWhenArrowsArePressed()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[1].GetComponent<Paddle>().MoveUpY("Paddle1");
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(0f, paddles[1].transform.position.y);
        }

        [UnityTest]
        public IEnumerator Paddle1CanMoveDownY()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[0].GetComponent<Paddle>().MoveDownY("Paddle1");
            yield return new WaitForFixedUpdate();

            Assert.AreNotEqual(0f, paddles[0].transform.position.y);
        }

        [UnityTest]
        public IEnumerator Paddle1CannotMoveDownWhenArrowsArePressed()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[0].GetComponent<Paddle>().MoveDownY("Paddle2");
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(0f, paddles[0].transform.position.y);
        }

        [UnityTest]
        public IEnumerator Paddle2CanMoveDownY()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[1].GetComponent<Paddle>().MoveDownY("Paddle2");
            yield return new WaitForFixedUpdate();

            Assert.AreNotEqual(0f, paddles[1].transform.position.y);
        }

        [UnityTest]
        public IEnumerator Paddle2CannotMoveDownWhenArrowsArePressed()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[1].GetComponent<Paddle>().MoveDownY("Paddle1");
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(0f, paddles[1].transform.position.y);
        }

        //not sure if these MoveX tests are right
        [UnityTest]
        public IEnumerator Paddle1CannotMoveInX()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[0].GetComponent<Paddle>().MoveX();
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(-6.0f, paddles[0].transform.position.x);
        }

        [UnityTest]
        public IEnumerator Paddle2CannotMoveInX()
        {
            GameObject[] paddles = _setup.CreatePaddlesForTest();

            Camera cam = _setup.CreateCameraForTest();

            paddles[1].GetComponent<Paddle>().MoveX();
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(6.0f, paddles[1].transform.position.x);
        }
    }
    
