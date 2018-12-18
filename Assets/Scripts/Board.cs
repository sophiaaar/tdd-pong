using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private BoardManager board;
    // Start is called before the first frame update
    void Start()
    {
        board = new BoardManager();
        board.CreatePaddles();
        board.CreateBall();
    }

    // Update is called once per frame
    void Update()
    {
        board.CheckForScore();
    }
}
