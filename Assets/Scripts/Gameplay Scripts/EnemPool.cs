using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemPool : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPreafbs;
    [SerializeField] private Transform enemyHolder;

    [SerializeField] List<GameObject> enemy0, enemy1, enemy2, enemy3, enemy4, enemy5 = new List<GameObject>();

    private int initilaPoolCount = 6;



    private void Awake()
    {
        CreateEnemyPool();
    }

    private void CreateEnemyPool()
    {
        for (int i = 0; i < enemiesPreafbs.Length; i++)
        {
            for (int j = 0; j < initilaPoolCount; j++)
            {
                if (ReturnList(i) == null)
                    return;

                ReturnList(i).Add(Instantiate(enemiesPreafbs[i], Vector3.zero, Quaternion.identity));
                ReturnList(i)[j].gameObject.transform.SetParent(enemyHolder);
                ReturnList(i)[j].SetActive(false);
            }
        }
    }

    public List<GameObject> ReturnList(int index)
    {
        switch (index)
        {
            case 0:
                return enemy0;
            case 1:
                return enemy1;
            case 2:
                return enemy2;
            case 3:
                return enemy3;
            case 4:
                return enemy4;
            case 5:
                return enemy5;
            default:
                return null;
        }
    }

    public int EnemyTypesCount()
    {
        return enemiesPreafbs.Length;
    }
}
