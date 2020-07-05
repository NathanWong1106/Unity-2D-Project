using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Components
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private Player player;
    
    //Movement Params
    private float speed = 1400f;
    private float jumpForce = 30f;
    private float horizontalInput;
    private bool jump;
    public bool isOnGround;
    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    //Update is called every frame
    private void Update()
    {
        //Input for combat and movement is received
        MovementInput();
        Combat();
    }

    // Fixed Update is called for physics updates
    void FixedUpdate()
    {
            //movement of the rigidbody using input received from Update()
            Movement();

    }

    //Receives movement input
    private void MovementInput()
    {
        //Input on the X axis
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //If the player has pressed space and they are on the ground then pass input to jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            jump = true;
            playerAnimator.SetTrigger("Jump");
        }

        //set the animation paramaters
        AnimParams(horizontalInput);

    }

    //Receives from MovementInput() and translates the playerRB
    private void Movement()
    {
        if (!paused)
        {
            //if 'jump' has been received, then apply an impulse force up
            if (jump)
            {
                playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jump = false;
            }
            //set the player's velocity in the x-axis
            playerRB.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, playerRB.velocity.y);
        } else
        {
            playerRB.velocity = Vector2.zero;
        }
    }

    //Sets the animation parameters in the Animator
    private void AnimParams(float horizontalInput)
    {
        if (!paused)
        {
            //set player speed
            playerAnimator.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));

            //set player direction
            if(horizontalInput > 0)
            {
                playerRenderer.flipX = false;
            }
            else if (horizontalInput < 0)
            {
                playerRenderer.flipX = true;
            }
        } else {
            playerAnimator.SetFloat("Speed", 0);
        }

    }

    //Receives combat input from the player and sets Animator paramaters
    private void Combat()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerAnimator.SetTrigger("Shoot");

            if (!playerRenderer.flipX)
            {
                player.Shoot(0);
            }
            else
            {
                player.Shoot(1);
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftControl) && isOnGround)
        {
            playerAnimator.SetTrigger("ShootUp");
            player.Shoot(2);
        }
    }
}
