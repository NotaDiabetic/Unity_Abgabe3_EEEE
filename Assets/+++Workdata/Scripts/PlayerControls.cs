using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8f;
    private Rigidbody2D rb;
    [SerializeField] private float jumpCount = 2;
    [SerializeField] private bool canDoubleJump = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount--;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (canDoubleJump == true)
            {
                jumpCount = 2;
            }
            else
            {
                jumpCount = 1;
            }

        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (canDoubleJump == true)
            {
                jumpCount = 1;
            }
            else
            {
                jumpCount = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hyperdrive"))
        {
            Debug.Log(message: "Waga Baga Bobo");
            canDoubleJump = true;
            Destroy(other.gameObject);
        }
    }



}
