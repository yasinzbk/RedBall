using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBall : MonoBehaviour
{
    public float speed = 5f;     
    public float jumpForce = 10f;   

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


        if (isGrounded)
        {

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                Jump();
            }

        }

    }


    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce); 
        isGrounded = false;
    }


    void OnCollisionEnter2D(Collision2D collision)  // sonsuz ziplamayi engelleme
    {
        // Zemin ile temas kontrolü
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            //   hizlanabilirMi = true;
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name == "Kutu")  // tag den yapmýyorz cunku ustune basip ziplamasi gerekiyor, tagi ground olacak
    //    {
    //        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //    }
    //}
}
