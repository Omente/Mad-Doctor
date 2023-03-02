using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    [SerializeField] private EnemyBullet[] enemyBulletPrefabs;
    [SerializeField] private Transform bulletsHolder;

    private EnemyBullet currentBullet;
    private List<EnemyBullet> enemyBullets0, enemyBullets1;

    private int bulletStartCount = 10;

    private bool bulletSetActive = false;

    private void Awake()
    {
        enemyBullets0 = new List<EnemyBullet>();
        enemyBullets1 = new List<EnemyBullet>();
        CreateBooletPool();
    }

    private void CreateBooletPool()
    {
        for (int i = 0; i < enemyBulletPrefabs.Length; i++)
        {
            switch (i)
            {
                case 0:
                    for (int j = 0; j < bulletStartCount; j++)
                    {
                        currentBullet = Instantiate(enemyBulletPrefabs[0], Vector3.zero, 
                            Quaternion.identity, bulletsHolder);

                        enemyBullets0.Add(currentBullet);
                        currentBullet.gameObject.transform.SetParent(bulletsHolder);
                        currentBullet.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    for (int j = 0; j <= bulletStartCount; j++)
                    {
                        currentBullet = Instantiate(enemyBulletPrefabs[i], Vector3.zero,
                            Quaternion.identity, bulletsHolder);
                        enemyBullets0.Add(currentBullet);
                        currentBullet.gameObject.transform.SetParent(bulletsHolder);
                        currentBullet.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void SetActiveBullet(int bulletType, Transform bulletSpawnPosition, bool setNegativeSpeed)
    {
        bulletSetActive = false;
        currentBullet = null;

        switch (bulletType)
        {
            case 0:
                foreach(var bullet in enemyBullets0)
                {
                    if(!bullet.gameObject.activeInHierarchy)
                    {
                        bullet.gameObject.SetActive(true);
                        bullet.transform.position = bulletSpawnPosition.transform.position;

                        if (setNegativeSpeed)
                            bullet.SetNegativeSpeed();

                        bulletSetActive = true;
                        break;
                    }
                }
                break;
            case 1:
                foreach (var bullet in enemyBullets1)
                {
                    if (!bullet.gameObject.activeInHierarchy)
                    {
                        bullet.gameObject.SetActive(true);
                        bullet.transform.position = bulletSpawnPosition.transform.position;

                        if (setNegativeSpeed)
                            bullet.SetNegativeSpeed();

                        bulletSetActive = true;
                        break;
                    }
                }
                break;
            default:
                break;
        }

        if (!bulletSetActive)
        {
            currentBullet = Instantiate(enemyBulletPrefabs[bulletType], bulletSpawnPosition.position, Quaternion.identity);

            if(bulletType == 0)
                enemyBullets0.Add(currentBullet);

            else if (bulletType == 1)
                enemyBullets1.Add(currentBullet);

            if (setNegativeSpeed)
                currentBullet.SetNegativeSpeed();

            currentBullet.gameObject.transform.SetParent(bulletsHolder);
        }
    }
}