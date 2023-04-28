using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPointTransforms;
    [SerializeField] private GameObject enemyPrefab;
    

    public int wave = 1;
    private IEnumerator coroutine;
    private int spawnPointsMaxedOut = 0;
    
    void Start()
    {
        for (int i = 0; i < spawnPointTransforms.Length; i++)
        {
            spawnPointTransforms[i].GetComponent<EnemySpawnPoint>().maxedOut = false;
            coroutine = SpawnEnemy(spawnPointTransforms[i], i);

            StartCoroutine(coroutine);
        }
    }

    private void Update()
    {
        if (spawnPointsMaxedOut == spawnPointTransforms.Length)
        {
            foreach (var point in spawnPointTransforms)
            {
                EnemySpawnPoint enemySpawnPoint = point.GetComponent<EnemySpawnPoint>();
                enemySpawnPoint.enemiesSpawned = 0;
                enemySpawnPoint.maxedOut = false;
                enemySpawnPoint.wasChecked = false;
            }

            spawnPointsMaxedOut = 0;
            wave += 1;
        }
        else
        {
            foreach (var point in spawnPointTransforms)
            {
                EnemySpawnPoint enemySpawnPoint = point.GetComponent<EnemySpawnPoint>();
                if (enemySpawnPoint.maxedOut && enemySpawnPoint.lastSpawnedEnemyHasDied)
                {
                    if (!enemySpawnPoint.wasChecked)
                    {
                        spawnPointsMaxedOut++;
                        enemySpawnPoint.wasChecked = true;
                    }
                }
            }
        }
    }

    public IEnumerator SpawnEnemy(Transform spawnPoint, float time)
    {
        EnemySpawnPoint enemySpawnPoint = spawnPoint.GetComponent<EnemySpawnPoint>();
        enemySpawnPoint.lastSpawnedEnemyHasDied = false;
        enemySpawnPoint.enemiesSpawned++;

        yield return new WaitForSeconds(time);

        enemySpawnPoint.spawnWarning.SetActive(true);
        enemySpawnPoint.anim.SetTrigger("Spawn");

        yield return new WaitForSeconds(2.33f);

        enemySpawnPoint.spawnWarning.SetActive(false);
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemyInstance.transform.SetParent(transform, true);
        enemyInstance.GetComponent<Enemy>().SpawnPoint = enemySpawnPoint;

        enemySpawnPoint.lastSpawnedEnemy = enemyInstance.GetComponent<Enemy>();
    }
}
