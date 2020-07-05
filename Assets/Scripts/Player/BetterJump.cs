using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to improve the jump feeling
public class BetterJump : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //if player is falling, increase gravity to speed up fall
        if(playerRB.velocity.y < 0)
        {
            //add gravity multiplier to downward force
            playerRB.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

        //if player is mid-jump and no longer holding down the jump button, then start increasing gravity
        else if (playerRB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            //add gravity multiplier to downward force
            playerRB.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
