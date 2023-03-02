using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int enemyHelath = 5;
    public int EnemyHealth { get { return enemyHelath; } set { enemyHelath = value; } }
    [SerializeField] private float playerMaxHealth = 100f;

    private float playerHealth;
    public float PlayerHealth { get { return playerHealth; } }

    private void Start()
    {
        playerHealth = playerMaxHealth;

        if (gameObject.CompareTag(TagManager.TAG_PLAYER))
        {
            GameplayUIController.intance.InitializeHealthSlider(0f, playerMaxHealth, playerHealth);
        }
    }

    public void EnemyTakeDamage(int amount)
    {
        enemyHelath -= amount;
    }

    public void PlayerTakeDamage(float amount)
    {
        playerHealth -= amount;

        GameplayUIController.intance.SetHealthSliderValue(playerHealth);
    }

    public void PlayerIncreaseHealth(float amount)
    {
        playerHealth += amount;
        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;

        GameplayUIController.intance.SetHealthSliderValue(playerHealth);
    }

}
