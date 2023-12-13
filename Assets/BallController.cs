using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;        // Topun hareket hýzý
    public float jumpForce = 10f;   // Zýplama kuvveti
    public float drag = 0.5f;       // Sürtünme miktarý

    private Rigidbody2D rb;  // mass: 5
    private bool isGrounded;
    //public bool hizlanabilirMi;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(horizontalInput, 0f);
        //if (hizlanabilirMi)
        //{
        if (isGrounded)
        {
            rb.AddForce(movement * speed);

        }
        else if (Mathf.Abs(rb.velocity.x) <= 5f)
        {
            rb.velocity = new Vector2(5f * movement.x, rb.velocity.y);
        }


        if ((horizontalInput * rb.velocity.x) <= 0)  // fren icin
        {
            rb.AddForce(movement * speed * 2);

        }




        if (isGrounded)
        {
            // Topu yavaþlatma (sürtünme uygulama)
            rb.velocity = rb.velocity * (1 - drag * Time.deltaTime);


            // Zýplama
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                Jump();
            }

        }

    }

    //void HizSinirla()
    //{
    //    hizlanabilirMi = false;
    //}

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Önceki dikey hýzý sýfýrla
        rb.AddForce(Vector2.up * jumpForce); //, ForceMode2D.Impulse
        isGrounded = false;

        //Invoke("HizSinirla", 0.5f);
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Kutu")  // tag den yapmýyorz cunku ustune basip ziplamasi gerekiyor, tagi ground olacak
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

}
