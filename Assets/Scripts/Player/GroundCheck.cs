using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private PlayerController playerController;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerAnim = GetComponentInParent<Animator>();
    }

    //TODO: add a ground tag to the base layer and check for validity here
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            playerController.isOnGround = true;
            playerAnim.SetBool("IsOnGround", true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            playerController.isOnGround = false;
            playerAnim.SetBool("IsOnGround", false);
        }

    }
}
