using UnityEditor.UI;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 8f;
    private Rigidbody2D rb;
    [SerializeField] private float jumpCount = 2;
    [SerializeField] public int Money = 0;
    [SerializeField] public int Gold = 0;
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool canMove = false;
    [SerializeField] private UiControls uiController;
    [SerializeField] private TimerScript tc;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (uiController.panelMainMenu.activeSelf == false && uiController.panelWon.activeSelf == false && uiController.panelLost.activeSelf == false && uiController.panelCountdown.activeSelf == false)
        {
            canMove = true;
        }
        if (canMove == true)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount--;
            }
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
         if (other.gameObject.CompareTag("Goal"))
        {
            if (Gold >= 3)
            {
                uiController.ShowPanelWon();
                tc.StopTimer();
                canMove = false;
            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 2 * jumpForce);
                Debug.Log(message: "Waga Baga Bobo");
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
            canDoubleJump = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Money"))
        {
            Money++;
            Destroy(other.gameObject);
            uiController.UpdateMoneyText(Money);
        }

        if (other.gameObject.CompareTag("Gold"))
        {
            Gold++;
            Destroy(other.gameObject);
            uiController.UpdateGoldText(Gold);
        }

        if (other.gameObject.CompareTag("Danger"))
        {
            Destroy(this.gameObject);
            uiController.ShowPanelLost();
            tc.StopTimer();
            canMove = false;
        }

    }



}
