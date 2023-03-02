using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUIController : MonoBehaviour
{
    public static GameOverUIController instance;

    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        gameOverCanvas.enabled = true;
        finalScoreText.text = $"Score: {GameplayUIController.intance.GetKillsCount()}";
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
