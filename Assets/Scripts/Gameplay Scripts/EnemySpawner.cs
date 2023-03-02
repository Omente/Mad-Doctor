using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint1, spawnPoint2;

    private int enemyWaveCount = 6;
    public int CurrentSpawnedEnemis {set; get;}
    private int enemyTypeCount;
    EnemPool enemyPool;

    private void Start()
    {
        enemyPool = gameObject.GetComponent<EnemPool>();
        enemyTypeCount = enemyPool.EnemyTypesCount();
    }

    private void Update()
    {
        CheackToSpawnNewWave();
    }

    private void SpawnWave()
    {

        List<GameObject> selectedList;
        Vector3 spawnPos;

        for (int i = 0; i < enemyWaveCount; i++)
        {
            selectedList = enemyPool.ReturnList(Random.Range(0, enemyTypeCount));

            foreach(var enemy in selectedList)
            {
                if(!enemy.activeInHierarchy)
                {
                    enemy.SetActive(true);
                    spawnPos = Random.Range(0, 2) > 0 ? spawnPoint1.position : spawnPoint2.position;
                    spawnPos.y = Random.Range(spawnPos.y - 1f, spawnPos.y + 2f);
                    enemy.transform.position = spawnPos;

                    CurrentSpawnedEnemis++;
                    break;
                }
            }
        }
    }

    private void CheackToSpawnNewWave()
    {
        if (CurrentSpawnedEnemis <= 0)
            SpawnWave();
    }
}
