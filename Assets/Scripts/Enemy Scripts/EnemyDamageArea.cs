using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageArea : MonoBehaviour
{
    [SerializeField] private float deactivateWaitTime = 0.1f;
    private float deactivateTimer;

    [SerializeField] private LayerMask playerLayerMask;

    private bool canDealDamage;

    private PlayerMovement player;

    private void Awake()
    {
        player = GameObject.FindWithTag(TagManager.TAG_PLAYER).GetComponent<PlayerMovement>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 1f, playerLayerMask))
        {
            if (canDealDamage)
            {
                canDealDamage = false;
                player.TakeDamage(10f);
            }
        }

        DeactivateDamageArea();
    }

    private void DeactivateDamageArea()
    {
        if (Time.time > deactivateTimer)
            gameObject.SetActive(false);
    }

    public void ResetDeactivateTimer()
    {
        canDealDamage = true;
        deactivateTimer = Time.time + deactivateWaitTime;
    }

}
