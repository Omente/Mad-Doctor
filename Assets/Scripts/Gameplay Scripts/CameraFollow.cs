using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 2f;
    [SerializeField] private float playerBoundMinY = -1f, playerBoundMinX = -65f, playerBoundMaxX = 65f;
    [SerializeField] private float yGap = 2f;

    private Vector3 temPos;
    private Transform playerTarget;

    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.TAG_PLAYER).transform;
    }

    private void Update()
    {
        if (!playerTarget)
            return;

        temPos = transform.position;

        if (playerTarget.position.y <= playerBoundMinY)
        {
            temPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y, -10f), smoothSpeed * Time.deltaTime);
        }
        else
        {
            temPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y + yGap, -10f), smoothSpeed * Time.deltaTime);
        }

        if (temPos.x < playerBoundMinX)
        {
            temPos.x = playerBoundMinX;
        }

        if (temPos.x > playerBoundMaxX)
        {
            temPos.x = playerBoundMaxX;
        }

        transform.position = temPos;
    }
}
