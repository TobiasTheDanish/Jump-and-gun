using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float healthDecrease = 1.1f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Animator anim;

    private int score = 0;
    private float points = 0;
    private float accuracy = 0f;
    private float shotsHit = 0;
    private float shotsFired = 0;
    private float originalHealthDecrease;
    private float maxHealth;

    public float ShotsFired { set => shotsFired = value; get => shotsFired; }
    public float Accuracy { get => accuracy; }

    // Start is called before the first frame update
    void Start()
    {
        originalHealthDecrease = healthDecrease;
        maxHealth = health;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            healthDecrease = Mathf.Pow(originalHealthDecrease, enemySpawner.wave);
            health -= Time.deltaTime * healthDecrease;
            healthBar.SetValue(health);
        }
        else
        {
            gameManager.PlayerDied(gameObject);
        }
    }

    public void EnemyHit(int healing, int score)
    {
        health += healing * enemySpawner.wave;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.SetValue(health);

        points += score;
        shotsHit++;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetValue(health);
        anim.SetTrigger("WasHit");
        AudioManager.Instance.Play("PlayerHit");
    }

    public int CalculateScore()
    {
        accuracy = CalculateAccuracy();
        float unroundedScore = points * accuracy;

        score = Mathf.RoundToInt(unroundedScore);

        return score;
    }

    private float CalculateAccuracy()
    {
        if (shotsFired == 0)
            return 0f;

        float _accuracy = (shotsHit / shotsFired) * 100f;
        float rounded = (float)Mathf.Round(_accuracy * 10f) / 10f;
        float output = rounded / 100f;
        return output;
    }
}
