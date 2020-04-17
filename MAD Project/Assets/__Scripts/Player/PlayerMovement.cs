using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // == public fields ==

    // == private fields ==
    private Rigidbody2D rb;
    
    private Camera gameCamera;
    [SerializeField]  private float jumpForce = 500f;

    [SerializeField] private float speed = 15.0f;

    // == private methods ==


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // add hMovement
        // if the player presses the up arrow, then move
        
        float vMovement = 0;
        float hMovement = 0;

        //The player movements
        if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow) ||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            hMovement= Input.GetAxis("Horizontal");
        }
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce));
        }

        // apply a force, get the player moving.
        rb.velocity = new Vector2(hMovement * speed, 
                                  vMovement * speed);
        // keep the player on the screen
        // float xValue = Mathf.clamp(value, min, max)
        // rb.position.x 
        float yValue = Mathf.Clamp(rb.position.y, -4.1f, 4.5f);
        float xValue = Mathf.Clamp(rb.position.x, -10.0f, 10.0f);

        rb.position = new Vector2(xValue, yValue);
    }
}
