using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private GameObject player;
    private float lerpVelocity = 7f;
    public bool shaking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // LateUpdate is called once per frame (last call)
    void LateUpdate()
    {
        if(player != null)
        {
            //calculate new position of the camera
            Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y / 2, transform.position.z);

            //Camera smoothing with lerp
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * lerpVelocity);
        }


    }
}
