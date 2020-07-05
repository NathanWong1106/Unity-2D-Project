using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, ILivingEntity
{

    //Game Components
    [SerializeField] private List<GameObject> projectileSpawns; // 0 - right, 1 - left, 2 - top
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem bloodSplash;
    private Animator playerAnim;
    private GameManager gameManager;

    //UI Components
    private GameObject gamePlayUI;
    private HealthBar healthbar;
    private Image canShootUI;
    private Image damagePowerupIndicator;

    //Params
    public float maxHealth = 100;
    public float health {get; set;}
    public float shootDelay = 0.8f;
    public float damage = 10f;
    private bool canShoot = true;
    //Powerup
    [SerializeField] private float damagePowerupDuration = 5f;
    private float damagePowerupTotalTime = 0f;
    private float damagePowerupAdditional = 10f;
    private bool damagePowerupActive = false;

    //Gather components on awake
    private void Awake()
    {
        health = maxHealth;
        playerAnim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gamePlayUI = GameObject.Find("Gameplay UI");
        healthbar = gamePlayUI.transform.GetChild(0).GetComponent<HealthBar>();
        canShootUI = gamePlayUI.transform.GetChild(1).GetComponent<Image>();
        damagePowerupIndicator = gamePlayUI.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(health);
    }
    //ILivingEntity, takes damage when called, triggers the Take Hit anim, and calls Camera Shake
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
        playerAnim.SetTrigger("Take Hit");
        StartCoroutine(Camera.main.gameObject.GetComponent<CameraMovement>().Shake(0.4f, 0.8f));

        if(health <= 0)
        {
            Instantiate(bloodSplash, transform.position, bloodSplash.transform.rotation);
            Destroy(gameObject);
            gameManager.OnPlayerDeath();
        }
    }

    //Shoots according to the specified spawn index: 0-right, 1-left, 2-top
    public void Shoot(int spawnIndex)
    {
        //If the player's shoot cooldown is not active then spawn the projectile
        if (canShoot)
        {
            //Instantiate a projectile
            GameObject bulletInst = Instantiate(bullet, projectileSpawns[spawnIndex].transform.position, bullet.transform.rotation);

            //Set the projectile damage
            bulletInst.GetComponent<Projectile>().damage = damage;

            //Which direction the projectile should move in
            switch (spawnIndex)
            {
                case 0:
                    bulletInst.GetComponent<Projectile>().direction = Vector3.right;
                    break;

                case 1:
                    bulletInst.GetComponent<Projectile>().direction = Vector3.left;
                    break;

                case 2:
                    bulletInst.GetComponent<Projectile>().direction = Vector3.up;
                    break;
            }

            //Activate the shoot cooldown
            StartCoroutine(delayShot());
        }
    }

    //Delays player shots
    private IEnumerator delayShot()
    {
        canShootUI.color = Color.gray;
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
        canShootUI.color = Color.white;
    }

    public IEnumerator DamagePowerup(GameObject powerup)
    {
        if (!damagePowerupActive)
        {
            damagePowerupIndicator.color = Color.white;
            damage += damagePowerupAdditional;
            damagePowerupTotalTime += damagePowerupDuration;
            damagePowerupActive = true;
            float elapsedTime = 0f;

            while(elapsedTime <= damagePowerupTotalTime)
            {

                elapsedTime += Time.deltaTime;
                yield return null;
                
            }

            yield return null;

            damagePowerupTotalTime = 0;
            damage -= damagePowerupAdditional;
            damagePowerupIndicator.color = Color.grey;
            Destroy(powerup);
        }
        else
        {
            damagePowerupTotalTime += damagePowerupDuration;
            Destroy(powerup);
        }
    }
}
