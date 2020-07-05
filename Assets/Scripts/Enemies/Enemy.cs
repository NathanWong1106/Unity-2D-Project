using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ILivingEntity
{

    //Enemy Params
    public float maxHealth = 20f;
    public float health {get; set;}
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float detectionDistance = 5f;
    [SerializeField] private float speed = 500f;

    //Navigation 
    [SerializeField] private GameObject rightCheck;
    [SerializeField] private GameObject leftCheck;
    private float wallCheckDistance = 0.2f;
    private float groundCheckDistance = 2f;
    private bool canAttack = true;
    private bool movingRight = true;
    private bool detection = false;
    private bool activateIdle = false;

    //Components
    [SerializeField] ParticleSystem bloodSplash;
    private GameObject player;
    private Animator enemyAnim;
    private Rigidbody2D enemyRB;
    private SpriteRenderer enemyRenderer;
    
    //Gather components on awake
    private void Awake()
    {
        health = maxHealth;
        enemyAnim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        enemyRB = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        EnemyMovementCheck();
    }

    private void EnemyMovementCheck()
    {
        RaycastHit2D wallCheck;
        RaycastHit2D groundCheck;
        activateIdle = false;

        //checks whether the player can be detected and in which direction the enemy should move towards the player
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= detectionDistance)
            {
                detection = true;
                if (player.transform.position.x > transform.position.x)
                {
                    movingRight = true;
                    enemyRenderer.flipX = false;
                }
                else
                {
                    movingRight = false;
                    enemyRenderer.flipX = true;
                }
            }
            else
            {
                detection = false;
            }
        }

        //Check for walls and ground to the right if the enemy is moving right
        if (movingRight)
        {
            wallCheck = Physics2D.Raycast(rightCheck.transform.position, Vector2.right, wallCheckDistance);
            groundCheck = Physics2D.Raycast(rightCheck.transform.position, Vector2.down, groundCheckDistance);

            if (wallCheck.collider != null || groundCheck.collider == null)
            {
                //if the player is within detection distance but there is an obstacle, idle the enemy
                if (detection)
                {
                    activateIdle = true;
                } else
                {
                    movingRight = false;
                    enemyRenderer.flipX = true;
                }

            }

        }
        //Check for walls and ground to the left if the enemy is moving left
        else
        {
            wallCheck = Physics2D.Raycast(leftCheck.transform.position, Vector2.left, wallCheckDistance);
            groundCheck = Physics2D.Raycast(leftCheck.transform.position, Vector2.down, groundCheckDistance);

            if (wallCheck.collider != null || groundCheck.collider == null)
            {
                //if the player is within detection distance but there is an obstacle, idle the enemy
                if (detection)
                {
                    activateIdle = true;
                }
                else
                {
                    movingRight = true;
                    enemyRenderer.flipX = false;
                }

            }
        }

        MoveEnemy();
    }

    private void MoveEnemy()
    {
        //if the enemy is not in idle state, then move to right/left
        if (!activateIdle)
        {
            if (movingRight)
            {
                enemyRB.velocity = new Vector2((speed * Time.deltaTime), enemyRB.velocity.y);
            } 
            else
            {
                enemyRB.velocity = new Vector2(-(speed * Time.deltaTime), enemyRB.velocity.y);
            }
        } else
        {
            //if the enemy is idling, set the x velocity to 0
            enemyRB.velocity = new Vector2(0, enemyRB.velocity.y);
        }

        //set anim params
        enemyAnim.SetFloat("Speed", Mathf.Abs(enemyRB.velocity.x));
    }

    //ILivingEntity, takes damage and checks for death
    public void TakeDamage(float damage)
    {
        health -= damage;
        enemyAnim.SetTrigger("Takehit");

        if(health <= 0)
        {
            Instantiate(bloodSplash, transform.position, bloodSplash.transform.rotation);
            Destroy(gameObject);
        }
    }

    //Sets the enemy attack cooldown
    private IEnumerator DelayAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    //Receives the player component from EnemyAttack.cs -- if the enemy's attack is not in cooldown, then damage the player and activate the attack anim trigger
    public void Attack(Player player)
    {
        if (canAttack)
        {
            enemyAnim.SetTrigger("Attack");
            player.TakeDamage(damage);
            StartCoroutine(DelayAttack());
        }
        
    }
}
