using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to destroy particle system GameObjects when they are done playing
public class DestroyParticle : MonoBehaviour
{
    private float timeToPlay;
    private ParticleSystem pSys;
    private void Awake()
    {
        pSys = GetComponent<ParticleSystem>();
        timeToPlay = pSys.main.duration;

        StartCoroutine(DeleteSystem());
    }

    IEnumerator DeleteSystem()
    {
        yield return new WaitForSeconds(timeToPlay);
        Destroy(gameObject);
    }
}
