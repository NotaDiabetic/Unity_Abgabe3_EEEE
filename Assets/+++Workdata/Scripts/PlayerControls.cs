using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    //sets the movement speed of the player
    [SerializeField] private float jumpForce = 8f;
    //sets the jumpForce of the player, aka how far up the player can jump
    private Rigidbody2D rb;
    //gives the script access to the RigidBody component of the player
    [SerializeField] private float jumpCount = 2;
    //sets the number of jumps that the player has available
    [SerializeField] public int Money = 0;
    //sets up the counter for the collected money
    [SerializeField] public int Gold = 0;
    //sets up the counter for the collected gold
    [SerializeField] private bool canDoubleJump = false;
    //decides if the player can double jump
    [SerializeField] private bool canMove = false;
    //decides if the player can move at all
    [SerializeField] private UiControls uiController;
    //gives this script access to the UIControls script
    [SerializeField] private TimerScript tc;
    //gives this script access to the TimerScript script


    void Start() //everything in the start function happens once when the game is initially started
    {
        rb = GetComponent<Rigidbody2D>();
        //when the game initially starts, takes access to the RigidBody component of the player
    }

    void Update() //everything in the update function gets repeated every frame
    {
        //if every 
        if (uiController.panelMainMenu.activeSelf == false && uiController.panelWon.activeSelf == false && uiController.panelLost.activeSelf == false && uiController.panelCountdown.activeSelf == false)
        {
            canMove = true;
            //allows the player to move if every ui panel is deactivated
        } 

        if (canMove == true) //the following only happens if the player is allowed to move
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
            //this is why the script needs access to the RigidBody
            //uses the unity input system to change the horizontal velocity based on the a/d or arrowkey left/right input, with the speed depending on the earlier established movement speed float

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 1) //this is for the jump. only triggers when the spacebar gets pressed AND the player still has jumps left
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount--;
                //accelerates the player upwards on the y axis based on the earlier established jumpForce float, then reduces the number of jumps the player has left by one
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) //everything here happens every time the player character's BoxCollider 2D enters another BoxCollider 2D
    {
        if (other.gameObject.CompareTag("Ground")) //this is what happens when the other object has the "Ground" Tag
        {
            if (canDoubleJump == true)
            {
                jumpCount = 2;
            }
            //if the player is allowed to double jump, the number of remaining jumps is set to 2
            else
            {
                jumpCount = 1;
            }
            //if the player is not allowed to double jump, the number of remaining jumps is only set to 1
        }
         if (other.gameObject.CompareTag("Goal")) //this is what happens when the other object has the "Goal" Tag
        {
            if (Gold >= 3)
            {
                uiController.ShowPanelWon();
                tc.StopTimer();
                canMove = false;
            } //if the player has 3 or more gold, the UiController shows the "You won" screen, the TimerScript stops the timer and the player is not allowed to move anymore
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 2 * jumpForce);
            } //if the player does not have 3 or more gold, he gets pushed away upwards on the y axis with twice the jumpForce
        }

    }

    private void OnCollisionExit2D(Collision2D other) //everything here happens every time the player character's BoxCollider 2D leaves another BoxCollider 2D
    {
        if (other.gameObject.CompareTag("Ground")) //this is what happens if the other object has the "Ground" Tag
        {
            if (canDoubleJump == true)
            {
                jumpCount = 1;
            } //if the player is allowed to double jump, the amount of remaining jumps is set to 1
            else
            {
                jumpCount = 0;
            } //if the player is not allowed to double jump, the amount of remaining jumps is set to 0
        } //this is to stop the player from making multiple mid air jumps after walking off an edge, or making mid air jumps at all if the player is not allowed to double jump yet
        
    }

    private void OnTriggerEnter2D(Collider2D other) //everything here happens every time the player character's BoxCollider 2D makes contact with another BoxCollider 2D that is marked as "trigger"
    {
        if (other.gameObject.CompareTag("Hyperdrive")) //this is what happens if the other object has the "Hyperdrive" tag
        {
            canDoubleJump = true;
            Destroy(other.gameObject);
            //from this moment on, the player is allowed to double jump, and the other object is destroyed
        }

        if (other.gameObject.CompareTag("Money")) //this is what happens if the other object has the "Money" tag
        {
            Money++;
            Destroy(other.gameObject);
            //first increases the Money counter by 1, then destroys the other object
            uiController.UpdateMoneyText(Money);
            //the uiController then updates the money counter on screen to keep track of collected money
        }

        if (other.gameObject.CompareTag("Gold")) //this is what happens if the other object has the "Gold" tag
        {
            Gold++;
            Destroy(other.gameObject);
            //first increases the Gold counter by 1, then destroys the other object
            uiController.UpdateGoldText(Gold);
             //the uiController then updates the gold counter on screen to keep track of collected gold
        }

        if (other.gameObject.CompareTag("Danger")) //this is what happens if the other object has the "Danger" tag
        {
            Destroy(this.gameObject);
            //the player character is destroyed
            uiController.ShowPanelLost();
            //the uiController shows the "you lost" panel
            tc.StopTimer();
            //the Timescript stops the timer
            canMove = false;
            //and the player is no longer allowed to move
        }

    }



}
