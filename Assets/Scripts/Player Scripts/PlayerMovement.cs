using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float shootWaitTime = 0.5f;
    [SerializeField] private float walkWaitTime = 0.3f;
    [SerializeField] private float damageColorWiteTime = 0.1f;
    [SerializeField] private Animator playerHurtFX;

    private bool canMove = true;
    private bool isDead = false;
    private bool playerDied = false;
    private bool playerDamaged = false;
    private float waitBeforeShooting;
    private float waitBeforeWalking;
    private float minBoundX = -68.8f, maxBounX = 70.4f, minBoundY = -4.13f, maxBoundY = -0.61f;
    private float xAxis, yAxis;
    private float damageColorTimer;
    private Vector3 tempPos;
    private Color tempColor;
    private PlayerAnimations playerAnimations;
    private PlayerShootingManager playerShootingManager;
    private Health playerHealth;
    private SpriteRenderer spriteRenderer;



    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
        playerHealth = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerDied)
            return;

        HandleMovment();
        HandleAnimation();
        FlipSprite();
        HandleShooting();
        CheackToMove();
        ChangeDamageColor();
    }

    private void HandleMovment()
    {
        xAxis = Input.GetAxisRaw(TagManager.AXIS_HORIZONTAL);
        yAxis = Input.GetAxisRaw(TagManager.AXIS_VERTICAL);

        if (!canMove)
            return;

        tempPos = transform.position;

        tempPos.x += moveSpeed * xAxis * Time.deltaTime;
        tempPos.y += moveSpeed * yAxis * Time.deltaTime;

        if (tempPos.x < minBoundX)
            tempPos.x = minBoundX;
        else if (tempPos.x > maxBounX)
            tempPos.x = maxBounX;

        if (tempPos.y < minBoundY)
            tempPos.y = minBoundY;
        else if (tempPos.y > maxBoundY)
            tempPos.y = maxBoundY;

        transform.position = tempPos;
    }

    private void HandleAnimation()
    {
        if (!canMove)
            return;

        if (Mathf.Abs(xAxis) > 0f || Mathf.Abs(yAxis) > 0f)
            playerAnimations.PlayAnimation(TagManager.ANIMATION_NAME_WLAK);
        else
            playerAnimations.PlayAnimation(TagManager.ANIMATION_NAME_IDLE);
    }

    private void FlipSprite()
    {
        if (xAxis == -1f)
            playerAnimations.ChangeFacingDirection(false);
        else if (xAxis == 1f)
            playerAnimations.ChangeFacingDirection(true);
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.K) && (Time.time > waitBeforeShooting) && playerShootingManager.GetWeaponType() != 0)
        {
            Shoot(false);
            return;
        }

        if (playerShootingManager.GetWeaponType() == 0)
        {
            if (Input.GetKey(KeyCode.K))
            {
                Shoot(true);
                canMove = false;
                waitBeforeWalking = float.PositiveInfinity;
            }
            else if (Input.GetKeyUp(KeyCode.K))
            {
                
                Shoot(false);
                waitBeforeShooting = 0f;
                canMove = true;
                waitBeforeWalking = Time.time + walkWaitTime;
            }
        }
    }

    private void Shoot(bool electrictyShoot)
    {
        if (playerShootingManager.GetWeaponType() != 0)
        {
            waitBeforeShooting = Time.time + shootWaitTime;
            StopMovment(walkWaitTime);
            playerShootingManager.ShootBullet(electrictyShoot);
            playerShootingManager.ShootBullet(transform.localScale.x);
            playerAnimations.PlayAnimation(TagManager.ANIMATION_NAME_SHOOT);
        }
        else
        {
            playerShootingManager.ShootBullet(electrictyShoot);
            playerAnimations.PlayAnimation(TagManager.ANIMATION_NAME_SHOOT);
        }
    }

    private void StopMovment(float waitTime)
    {
        canMove = false;
        waitBeforeWalking = Time.time + waitTime;
    }

    private void CheackToMove()
    {
        if (Time.time < waitBeforeWalking)
            return;

        canMove = true;
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        playerHealth.PlayerTakeDamage(amount);

        if (playerHealth.PlayerHealth <= 0)
        {
            playerDied = true;
            playerAnimations.PlayAnimation(TagManager.ANIMATION_NAME_DEATH);

            Invoke("RemovePlayerFromGame", 3f);
        }
        else
        {
            PlayerReciveDamage();
        }
    }

    private void RemovePlayerFromGame()
    {
        GameOverUIController.instance.GameOver();
        Destroy(gameObject);
    }

    private void PlayerReciveDamage()
    {
        if (!playerDamaged)
        {
            tempColor = spriteRenderer.material.color;
            tempColor.a = 1f;
            tempColor.r = 1f;
            tempColor.g = 0f;
            tempColor.b = 0f;

            spriteRenderer.material.SetColor("_Color", tempColor);

            damageColorTimer = Time.time + damageColorWiteTime;
            playerDamaged = true;

            playerHurtFX.Play(TagManager.ANIMATION_NAME_FX);
            //play hurt sound effect
        }
    }

    private void ChangeDamageColor()
    {
        if(playerDamaged)
        {
            if (Time.time > damageColorTimer)
            {
                playerDamaged = false;

                tempColor = spriteRenderer.material.color;
                tempColor.a = 1f;
                tempColor.r = 1f;
                tempColor.g = 1f;
                tempColor.b = 1f;

                spriteRenderer.material.SetColor("_Color", tempColor);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.TAG_ENEMY_BULLET))
        {
            TakeDamage(20f);
        }

        if (collision.gameObject.CompareTag(TagManager.TAG_HEALTH_FUEL_TAG))
        {
            playerHealth.PlayerIncreaseHealth(collision.gameObject.GetComponent<HealthFuel>().GetHealthValue());
            Destroy(collision.gameObject);
        }
    }
}