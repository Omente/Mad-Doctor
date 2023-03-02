using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    

    private int initialBulletCount = 10;

    [SerializeField] private List<Bullet> weapon_1_BulletPool, weapon_2_BulletPool, weapon_3_BulletPool = new List<Bullet>();
    [SerializeField] private Transform bulletHolder;

    public void CreateBulletPool(int weaponType, Bullet bullet)
    {
        GameObject newBullet = null;

        if(weaponType == 0)
        {
            for (int i = 0; i < initialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);
                newBullet.transform.SetParent(bulletHolder);
                weapon_1_BulletPool.Add(newBullet.GetComponent<Bullet>());
                newBullet.SetActive(false);
            }
        }        
        else if(weaponType == 1)
        {
            for (int i = 0; i < initialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);
                newBullet.transform.SetParent(bulletHolder);
                weapon_2_BulletPool.Add(newBullet.GetComponent<Bullet>());
                newBullet.SetActive(false);
            }
        }
        else if(weaponType == 2)
        {
            for (int i = 0; i < initialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);
                newBullet.transform.SetParent(bulletHolder);
                weapon_3_BulletPool.Add(newBullet.GetComponent<Bullet>());
                newBullet.SetActive(false);
            }
        }

    }

    public void AddBulletToPool(int weaponType, Bullet bullet)
    {
        if(weaponType == 0)
            weapon_1_BulletPool.Add(bullet);
        else if(weaponType == 1)
            weapon_1_BulletPool.Add(bullet);
        else if(weaponType == 2)
            weapon_1_BulletPool.Add(bullet);

        bullet.transform.SetParent(bulletHolder);
    }

    public Bullet GetBullet(int weaponType)
    {
        if(weaponType == 0)
        {
            foreach (Bullet bullet in weapon_1_BulletPool)
            {
                if(!bullet.gameObject.activeInHierarchy)
                    return bullet;
            }
        }
        else if(weaponType == 1)
        {
            foreach (Bullet bullet in weapon_2_BulletPool)
            {
                if(!bullet.gameObject.activeInHierarchy)
                    return bullet;
            }
        }
        else if(weaponType == 2)
        {
            foreach (Bullet bullet in weapon_3_BulletPool)
            {
                if(!bullet.gameObject.activeInHierarchy)
                    return bullet;
            }
        }

        return null;
    }

}
