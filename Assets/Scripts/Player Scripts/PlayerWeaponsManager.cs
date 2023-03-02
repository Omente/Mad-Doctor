using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerShootingManager playerShootingManager;

    [SerializeField] private GameObject electricBullet;

    private int weaponIndex;
    private int numberOfWeapons;

    private void Awake()
    {
        playerShootingManager = GetComponent<PlayerShootingManager>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        numberOfWeapons = playerAnimations.GetNumberOfWeapons();
        weaponIndex = 0;

        playerAnimations.ChangeAnimatorController(weaponIndex);
        playerShootingManager.SetWeaponType(weaponIndex);
    }

    private void Update()
    {
        ChangeWeaopon();
    }

    private void ChangeWeaopon()
    {
        GameplayUIController.intance.ChangeWeaponIcon(weaponIndex, false);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponIndex++;
            if (weaponIndex == numberOfWeapons)
                weaponIndex = 0;
            playerAnimations.ChangeAnimatorController(weaponIndex);

            if (weaponIndex != 0)
                electricBullet.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            weaponIndex--;
            if (weaponIndex < 0)
                weaponIndex = numberOfWeapons - 1;
            playerAnimations.ChangeAnimatorController(weaponIndex);

            if (weaponIndex != 0)
                electricBullet.SetActive(false);
        }


        GameplayUIController.intance.ChangeWeaponIcon(weaponIndex, true);
        SetWeaponIndex(weaponIndex);
    }

    private void SetWeaponIndex(int weaponIndex)
    {
        playerShootingManager.SetWeaponType(weaponIndex);
    }
}