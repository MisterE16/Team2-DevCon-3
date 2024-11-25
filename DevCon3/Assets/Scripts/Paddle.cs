using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayerOne;
    public float speed;
    public Rigidbody2D rb;
    private float movement;

   
    void Update()
    {
       if (isPlayerOne)
        {
            movement = Input.GetAxisRaw("Vertical");
        }
       else
        {
            movement = Input.GetAxisRaw("Vertical2");
        }

       rb.velocity = new Vector2 (rb.velocity.x, movement * speed);
    }
}
