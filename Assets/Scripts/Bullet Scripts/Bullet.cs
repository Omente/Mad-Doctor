using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private bool getTrailRenderer;

    private Vector3 moveVector = Vector3.zero;
    private Vector3 tempScale = Vector3.one;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        if (getTrailRenderer)
            trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    private void OnDisable()
    {
        RestartBulletParameters();
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

    private void RestartBulletParameters()
    {
        moveVector = Vector3.zero;
        moveSpeed = Mathf.Abs(moveSpeed);

        tempScale = transform.localScale;
        tempScale.x = Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;
        if (getTrailRenderer)
            trailRenderer.Clear();
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
        if (collision.gameObject.CompareTag(TagManager.TAG_ENEMY))
        {
            gameObject.SetActive(false);
        }
    }
}
