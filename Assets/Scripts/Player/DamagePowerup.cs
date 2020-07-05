using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DamagePowerup : MonoBehaviour
{
    [SerializeField] private float additionalDamage = 10f;
    [SerializeField] private float powerupTime = 8f;
    [SerializeField] private ParticleSystem damagePowerupParticle;
    [SerializeField] private float floatingMagnitude = 0.5f;
    private float yPos = 0;
    private float originalYPos;

    private void Awake()
    {
        originalYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        //Moves the powerup up and down in a wave
        transform.position = new Vector2(transform.position.x, originalYPos + (Mathf.Sin(yPos) * floatingMagnitude));
        yPos += Time.deltaTime;

        if(yPos >= 360)
        {
            yPos = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //If the player collides with the powerup trigger, activate the powerup and disappear from view
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            StartCoroutine(player.DamagePowerup(gameObject));

            Instantiate(damagePowerupParticle, transform.position, damagePowerupParticle.transform.rotation);

            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Light2D>().enabled = false;
        }
    }
}
