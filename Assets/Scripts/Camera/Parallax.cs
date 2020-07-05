using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length;
    [SerializeField] private float heightOffset = 0.5f;
    [SerializeField]private float startPos;
    [SerializeField] private float parallaxEffect = 1f; //1f will follow the camera 1:1
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //distance the sprite is away from the camera
        float distFromCam = Camera.main.transform.position.x * (1 - parallaxEffect);

        //distance the sprite should move away from the camera
        float dist = (Camera.main.transform.position.x * parallaxEffect);

        //move the sprite away from its original position by adding the distance to its original starting position
        transform.position = new Vector3(startPos + dist, Camera.main.transform.position.y * parallaxEffect + heightOffset, transform.position.z);


        //if the sprite is too far right from its original position
        if(distFromCam > startPos + length)
        {
            startPos += length;
        }

        //if the sprite is too far left from its original position
        else if (distFromCam < startPos - length)
        {
            startPos -= length;

        }

    }
}
