using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    private IEnumerator coroutine;

    public Animator anim;
    public GameObject spawnWarning;
    public Enemy lastSpawnedEnemy;
    public bool lastSpawnedEnemyHasDied = false;
    public bool maxedOut = true;
    public int enemiesSpawned = 0;
    public bool wasChecked = false;

    private void Update()
    {
        if(enemiesSpawned >= enemySpawner.wave)
        {
            maxedOut = true;
        }

        if (!maxedOut && lastSpawnedEnemyHasDied)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float time = Random.Range(.3f, 3f);
        
        coroutine = enemySpawner.SpawnEnemy(transform, time);
        StartCoroutine(coroutine);
        lastSpawnedEnemyHasDied = false;
    }
}
