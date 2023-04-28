using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private AIPath aiPath;
    [SerializeField] private GameObject eyes;
    [SerializeField] private float eyeMovement;
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;
    [SerializeField] [Range(.5f,1.5f)] private float attackRange;
    [SerializeField] private GameObject visuals;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ParticleSystem death;
    [SerializeField] private LayerMask whatIsPlayer;
    public EnemySpawnPoint SpawnPoint;

    private Animator anim;
    private int maxHealth;
    private float timeBtwDealingDamage = 1f;
    private float timeSinceLastDealingDamage = 0;

    private void Start()
    {
        maxHealth = health;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(health);
        anim = visuals.GetComponent<Animator>();
        destinationSetter.target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            eyes.transform.localPosition = new Vector3(eyeMovement, eyes.transform.localPosition.y);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            eyes.transform.localPosition = new Vector3(-eyeMovement, eyes.transform.localPosition.y);
        }
        else
        {
            eyes.transform.localPosition = new Vector3(0, eyes.transform.localPosition.y);
        }

        if (timeSinceLastDealingDamage >= timeBtwDealingDamage)
        {
            DealDamage();   
        }
        else
        {
            timeSinceLastDealingDamage += Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hit");
        health -= damage;
        healthBar.SetValue(health);

        if (health <= 0)
        {
            aiPath.isStopped = true;
            death.Play();

            SpawnPoint.lastSpawnedEnemyHasDied = true;

            healthBar.gameObject.SetActive(false);
            visuals.SetActive(false);
            timeSinceLastDealingDamage = 0f;
            Invoke("DestroySelf", .2f);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void DealDamage()
    {
        Vector3 dir = (destinationSetter.target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, attackRange, whatIsPlayer);
        if (hit)
        {
            Player player = hit.collider.gameObject.GetComponentInParent<Player>();
            player.TakeDamage(attackDamage);
            timeSinceLastDealingDamage = 0f;
        }
    }
}
