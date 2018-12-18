using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    //public GameObject paddlePrefab;
    //public GameObject ballPrefab;
    private int ballCount = 0;
    public int playerOneScore = 0;
    public int playerTwoScore = 0;

    public GameObject[] CreatePaddles()
    {
        GameObject paddle1 = new GameObject("Paddle1");
        paddle1.transform.position = new Vector3(-6, 0, 0);
        paddle1.AddComponent<Paddle>();

        GameObject paddle2 = new GameObject("Paddle2");
        paddle2.transform.position = new Vector3(6, 0, 0);
        paddle2.AddComponent<Paddle>();
        
        GameObject[] paddles = { paddle1, paddle2 };

        return paddles;
    }

    public GameObject CreateBall()
    {
        if (ballCount == 0)
        {
            ballCount++;
            GameObject ball = new GameObject("Ball");
            ball.AddComponent<Ball>();
            return ball;
        }
        return null;
    }

    void GetBounds()
    {
        int screenHeight = Screen.height;
        int screenWeight = Screen.width;
    }

    public void CheckForScore()
    {
        GameObject ball = GameObject.Find("Ball");
        if (ball.transform.position.y >= 7)
        {
            playerOneScore++;
            ball.GetComponent<Ball>().ResetBall(Ball.serviceDirection.right);
        }
        else if ( ball.transform.position.y <= -7 )
        {
            playerTwoScore++;
            ball.GetComponent<Ball>().ResetBall(Ball.serviceDirection.right);

        }
    }
}
