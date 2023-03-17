using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIController : MonoBehaviour
{
    public static GameplayUIController intance;

    [SerializeField] private TextMeshProUGUI killScoreText;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private GameObject[] weaponIcons;

    private int killScoreCount;
    private int weaponIconIndex;
    
    

    private void Awake()
    {
        if(!intance)
        {
            intance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(intance);
        }
    }

    public void InitializeHealthSlider(float minHealthValue, float maxHealthValue, float currentHealthValue)
    {
        playerHealthSlider.maxValue = maxHealthValue;
        playerHealthSlider.minValue = minHealthValue;
        playerHealthSlider.value = currentHealthValue;
    }

    public void SetHealthSliderValue(float newValue)
    {
        playerHealthSlider.value = newValue;
    }

    public void ChangeWeaponIcon(int index, bool activate)
    {
        weaponIcons[index].SetActive(activate);
    }


    public void SetKillScoreText()
    {
        killScoreCount++;
        killScoreText.text = $"Kills: {killScoreCount}";
    }

    public int GetKillsCount()
    {
        return killScoreCount;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}