using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Projectile : MonoBehaviour
{

    //Params
    public Vector3 direction = Vector3.zero; //initialized with no direction
    private Vector3 originalPos;
    public float damage = 10f;
    [SerializeField] private float speed = 25f;
    bool impact = false;
    
    //Components
    private Animator bulletAnim;
    private Light2D light2D;
    [SerializeField] private ParticleSystem bulletImpact;
    private CapsuleCollider2D capsule;
    
    

    private void Awake()
    {
        originalPos = transform.position;
        bulletAnim = GetComponent<Animator>();
        light2D = GetComponent<Light2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, originalPos) > 15 && !impact)
        {
            StartCoroutine(Impact());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if the collision object implements the ILivingEntity interface, then damage the entity
        ILivingEntity other = collision.gameObject.GetComponent<ILivingEntity>();

        

        if (other != null && collision.collider.CompareTag("Enemy"))
        {
            other.TakeDamage(damage);
        }

        //destroy the projectile
        StartCoroutine(Impact());
    }

    private IEnumerator Impact()
    {
        //stop bullet and trigger the impact animation
        impact = true;
        capsule.enabled = false;
        speed = 0;
        bulletAnim.SetTrigger("Impact");
        light2D.pointLightOuterRadius = 1.5f;
        Instantiate(bulletImpact, transform.position, bulletImpact.transform.rotation);

        //wait for animation to play and then destroy the game object
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);

    }
}
