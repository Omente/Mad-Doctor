using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFuelSpawner : MonoBehaviour
{
    [SerializeField] private int chanceToSpawn = 3;
    [SerializeField] private GameObject[] healthFuels;

    private Health enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
    }

    public void SpawnHealth()
    {
        if (enemyHealth.EnemyHealth != 0 && enemyHealth.EnemyHealth != 5)
            return;

        if (Random.Range(0, 10) > chanceToSpawn)
        {
            Instantiate(healthFuels[Random.Range(0, healthFuels.Length)], transform.position, Quaternion.identity);
        }
    }

    
}
