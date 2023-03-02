using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum EnemyState
{
    Idle,
    Attack,
    Walk,
    Hit,
    Electric,
    Death
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool isShooter;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackerStopingDistance = 1.5f, shooterStopingDistance = 8f;
    [SerializeField] private float damageWaitTime = 0.5f;
    [SerializeField] private float attackWaitTime = 2.5f;
    [SerializeField] private float attackFinishWaitTime = 0.5f;
    [SerializeField] private EnemyDamageArea enemyDamageArea;
    [SerializeField] private EnemyBullet enemyBullet;
    [SerializeField] [Range(0, 1)] private int bulletType;
    [SerializeField] private Transform enemyBulletSpawnPos;

    private bool enemyDead;
    private float stoppingDistance;
    private float damageTimer;
    private float attackTimer;
    private float attackFinishedTimer;
    private Vector3 tempScale;
    private Transform playerTarget;
    private PlayerAnimations enemyAnimations;
    private EnemyState enemyStateEnum;
    private EnemyBulletPool enemyBulletPool;
    private Health enemyHealth;
    private HealthFuelSpawner healthFuelSpawner;
    

    private void Awake()
    {
        playerTarget = GameObject.FindWithTag(TagManager.TAG_PLAYER).transform;
        enemyAnimations = GetComponent<PlayerAnimations>();
        enemyHealth = GetComponent<Health>();
        healthFuelSpawner = GetComponent<HealthFuelSpawner>();
    }

    private void Start()
    {
        if (isShooter)
        {
            stoppingDistance = shooterStopingDistance;
            enemyBulletPool = GameObject.FindWithTag(TagManager.TAG_ENEMY_BULLET_POOL).GetComponent<EnemyBulletPool>();
        }
        else
            stoppingDistance = attackerStopingDistance;
    }

    private void Update()
    {
        if (enemyDead)
            return;

        AnimateEnemy();
        SearchForPlayer();
    }

    private void OnEnable()
    {
        enemyDead = false;
    }

    private void AnimateEnemy()
    {
        if (enemyStateEnum == EnemyState.Idle)
            enemyAnimations.PlayAnimation(TagManager.ANIMATION_NAME_IDLE);

        if (enemyStateEnum == EnemyState.Attack)
            enemyAnimations.PlayAnimation(TagManager.ANIMATION_NAME_ATTACK);

        if (enemyStateEnum == EnemyState.Death)
        {
            enemyDead = true;
            enemyAnimations.PlayAnimation(TagManager.ANIMATION_NAME_DEATH);
        }

        if (enemyStateEnum == EnemyState.Electric)
            enemyAnimations.PlayAnimation(TagManager.ANIMATION_NAME_ELECTIRC);

        if (enemyStateEnum == EnemyState.Hit)
            enemyAnimations.PlayAnimation(TagManager.ANIMATION_NAME_HIT);

        if (enemyStateEnum == EnemyState.Walk)
            enemyAnimations.PlayAnimation(TagManager.ANIMATION_NAME_WLAK);
    }

    private void SearchForPlayer()
    {
        if (enemyStateEnum == EnemyState.Death)
            return;

        if (!playerTarget)
        {
            enemyStateEnum = EnemyState.Idle;
            return;
        }

        if (enemyStateEnum == EnemyState.Hit)
        {
            CheackIfDamageIsOver();
            return;
        }

        if (enemyStateEnum == EnemyState.Electric)
            return;

        if (Vector3.Distance(transform.position, playerTarget.transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
            HandleFacingDirection();
            enemyStateEnum = EnemyState.Walk;
        }
        else
        {
            HandleFacingDirection();
            CheackIfAttackFinished();
            Attack();
        }
    }

    private void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (playerTarget.position.x > transform.position.x)
            tempScale.x = -Mathf.Abs(tempScale.x);
        else
            tempScale.x = Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;
    }

    private void CheackIfAttackFinished()
    {
        if (Time.time > attackFinishedTimer)
        {
            enemyStateEnum = EnemyState.Idle;
        }
    }

    private void Attack()
    {
        if (Time.time > attackTimer)
        {
            attackFinishedTimer = Time.time + attackFinishWaitTime;
            attackTimer = Time.time + attackWaitTime;

            enemyStateEnum = EnemyState.Attack;

            if (isShooter)
            {
                if(transform.position.x > playerTarget.transform.position.x)
                {
                    //set negative speed here;
                    //Instantiate(enemyBullet, enemyBulletSpawnPos.position, Quaternion.identity).SetNegativeSpeed();
                    enemyBulletPool.SetActiveBullet(bulletType, enemyBulletSpawnPos, true);
                }
                else
                {
                    //Instantiate(enemyBullet, enemyBulletSpawnPos.position, Quaternion.identity);
                    enemyBulletPool.SetActiveBullet(bulletType, enemyBulletSpawnPos, false);
                }
            }


        }
    }

    private void EnemyAttackerAttacked()
    {
        enemyDamageArea.gameObject.SetActive(true);
        enemyDamageArea.ResetDeactivateTimer();
    }

    private void EnemyDamage(bool electricDamage)
    {
        if (electricDamage)
        {
            enemyStateEnum = EnemyState.Electric;
            DealDamage(2);
        }
        else
        {
            damageTimer = Time.time + damageWaitTime;
            enemyStateEnum = EnemyState.Hit;
            DealDamage(1);
        }
    }

    private void CheackIfDamageIsOver()
    {
        if (Time.time > damageTimer)
            enemyStateEnum = EnemyState.Idle;
    }

    private void DealDamage(int amount)
    {
        enemyHealth.EnemyTakeDamage(amount);

        if(enemyHealth.EnemyHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;

            enemyStateEnum = EnemyState.Death;

            GameplayUIController.intance.SetKillScoreText();

            healthFuelSpawner.SpawnHealth();

            Invoke("DisableEnemyFromGame", 3f);
        }
    }

    private void DisableEnemyFromGame()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.TAG_PLAYER_BULLET))
        {
            EnemyDamage(false);
        }
        else if (collision.gameObject.CompareTag(TagManager.TAG_ELECTRIC_BULLET))
        {
            EnemyDamage(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.TAG_ELECTRIC_BULLET))
        {
            enemyStateEnum = EnemyState.Idle;
        }
    }

    private void OnDisable()
    {

        GameObject.FindWithTag(TagManager.TAG_ENEMY_SPAWNER).GetComponent<EnemySpawner>().CurrentSpawnedEnemis--;

        if (GameObject.FindWithTag(TagManager.TAG_ENEMY_SPAWNER).GetComponent<EnemySpawner>().CurrentSpawnedEnemis < 0)
            GameObject.FindWithTag(TagManager.TAG_ENEMY_SPAWNER).GetComponent<EnemySpawner>().CurrentSpawnedEnemis = 0;

        enemyHealth.EnemyHealth = 5;
    }
}