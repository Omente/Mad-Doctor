using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    public void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
        finalScoreText.text = $"Score: {GameplayUIController.intance.GetKillsCount()}";
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void OnSceneChanged(Scene scene, Scene mode)
    {
        Debug.Log("Start");
        gameOverCanvas.gameObject.SetActive(false);
        Debug.Log("End");
    }
}
