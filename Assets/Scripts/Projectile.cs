using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int healing;
    [SerializeField] private int score = 5;
    [SerializeField] private int damage;
    [SerializeField] private float force;
    [SerializeField] private float knockback;

    private Rigidbody2D rb;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            player.EnemyHit(healing, score);

            collision.rigidbody.AddForce(transform.up * knockback);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }

    }
}
