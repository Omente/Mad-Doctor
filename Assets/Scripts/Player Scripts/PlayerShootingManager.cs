using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private Transform[] bulletSpawnPosition;
    [SerializeField] private GameObject electricityBullet;
    [SerializeField] private Animator bulletFX0, bulletFX1;

    private int weaponType;
    private BulletPool bulletPool;
    private Bullet currentBullet;

    private void Awake()
    {
        bulletPool = GetComponent<BulletPool>();
        InitializeBullets();

    }

    private void InitializeBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bulletPool.CreateBulletPool(i, bullets[i]);
        }
    }

    public void SetWeaponType(int newType)
    {
        weaponType = newType;
    }

    public int GetWeaponType()
    {
        return weaponType;
    }

    public void ShootBullet(float facingLeftSide)
    {
        int currentWeaponType = weaponType;

        if (currentWeaponType != 0)
        {
            currentWeaponType--;

            currentBullet = bulletPool.GetBullet(currentWeaponType);

            if (currentBullet)
            {
                currentBullet.gameObject.SetActive(true);
                currentBullet.gameObject.transform.position = bulletSpawnPosition[currentWeaponType].position;
            }
            else
            {
                currentBullet = Instantiate(bullets[currentWeaponType]);
                currentBullet.gameObject.transform.position = bulletSpawnPosition[currentWeaponType].position;
                bulletPool.AddBulletToPool(currentWeaponType, currentBullet);
            }

            if (facingLeftSide < 0)
            {
                currentBullet.SetNegativeSpeed();
            }

            if (currentWeaponType != 2)
            {
                bulletFX0.gameObject.transform.position = bulletSpawnPosition[currentWeaponType].position;
                bulletFX0.Play(TagManager.ANIMATION_NAME_FX);
            }
            else
            {
                bulletFX1.gameObject.transform.position = bulletSpawnPosition[currentWeaponType].position;
                bulletFX1.Play(TagManager.ANIMATION_NAME_FX);
            }
        }
    }

    public void ShootBullet(bool shootElectricty)
    {
        int currentWeaponType = weaponType;

        if (currentWeaponType == 0)
            ShootElectricty(shootElectricty);
    }



    private void ShootElectricty(bool activateWeapon)
    {
        electricityBullet.SetActive(activateWeapon);
    }
}
