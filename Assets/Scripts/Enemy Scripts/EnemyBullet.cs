using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;

    private Vector3 moveVector;
    private Vector3 tempScale;

    [SerializeField] private bool playSound1;

    private void Start()
    {
        if (playSound1)
        {
        }
        else
        {
        }
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        moveVector.x = moveSpeed * Time.deltaTime;
        transform.position += moveVector;
    }

    public void SetNegativeSpeed()
    {
        moveSpeed = -Mathf.Abs(moveSpeed);

        tempScale = transform.localScale;
        tempScale.x = -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag(TagManager.TAG_ENEMY_BULLET) && collision.CompareTag(TagManager.TAG_PLAYER))
        {
            gameObject.SetActive(false);
        }
    }
}
