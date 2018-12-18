using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestSetup
{
    public GameObject[] CreatePaddlesForTest()
        {
            BoardManager board = new BoardManager();
            GameObject[] paddles = board.CreatePaddles();
            return paddles;
        }

        public Camera CreateCameraForTest()
        {
            Camera cam = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Main Camera.prefab", typeof(Camera)) as Camera;
            GameObject.Instantiate(cam, new Vector3(0, 0, -10), Quaternion.identity);
            return cam;
        }

        public GameObject CreateBallForTest()
        {
            BoardManager board = new BoardManager();
            GameObject ball = board.CreateBall();
            return ball;
        }

        public void DestroyAll()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
                GameObject.DestroyImmediate(o);
            }
        }

}
