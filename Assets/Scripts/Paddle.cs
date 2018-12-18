using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Paddle : MonoBehaviour
{
    private Vector2 speed;

    void Update()
    {
        GetInput();
    }

    void Start()
    {
        RenderPaddle();
        AddCollider();
        SetTagOnPaddle();
        AddRigidbody();
    }

    public void SetSpeed(float x, float y)
    {
        speed = new Vector2(x,y);
    }
    public Vector2 GetSpeed()
    {
        return speed;
    }

    public void AddCollider()
    {
        this.gameObject.AddComponent<BoxCollider2D>();
        //this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void AddRigidbody()
    {
        this.gameObject.AddComponent<Rigidbody2D>();
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void RenderPaddle()
    {
        this.gameObject.transform.localScale = new Vector3(0.5f, 2.0f, 1);
        this.gameObject.AddComponent<SpriteRenderer>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/whitesquare.png", typeof(Sprite));
    }

    public void SetTagOnPaddle()
    {
        this.gameObject.tag = "Paddle";
    }

    void GetInput()
    {
        //this can probably be more streamlined!!!

        if (Input.GetAxis("Vertical1") > 0)
        {
            MoveUpY("Paddle1");
        }
        else if (Input.GetAxis("Vertical1") < 0)
        {
            MoveDownY("Paddle1");
        }
        if (Input.GetAxis("Vertical2") > 0)
        {
            MoveUpY("Paddle2");
        }
        else if (Input.GetAxis("Vertical2") < 0)
        {
            MoveDownY("Paddle2");
        }
    }

    public Vector3 MoveUpY(string paddleName)
    {
        if (paddleName == this.name)
        {
            if (transform.position.y < (Camera.main.orthographicSize - (transform.localScale.y /2)))
            {
                transform.position += Vector3.up * Time.deltaTime * 5;
                return transform.position;
            }
        }
        return transform.position;
    }

    public Vector3 MoveDownY(string paddleName)
    {
        if (paddleName == this.name)
        {
            if (transform.position.y > -(Camera.main.orthographicSize - (transform.localScale.y /2)))
            {
                transform.position -= Vector3.up * Time.deltaTime * 5;
                return transform.position;
            }
        }
        return transform.position;
    }

    public Vector3 MoveX()
    {
        return transform.position;
    }


}
