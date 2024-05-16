using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Fruit counters")]
    [SerializeField] private TextMeshProUGUI bananaCounterText;
    [SerializeField] private TextMeshProUGUI pineappleCounterText;
    [SerializeField] private TextMeshProUGUI cherryCounterText;

    [Header("Lives Display")]
    [SerializeField] private Image[] lifeIcons = new Image[3];

    [Header("Game over screen")]
    [SerializeField] private GameObject gameOverScreenObject;
    [SerializeField] private Button gameOverRestartButton;
    [SerializeField] private Button backMenuButton1;
    [SerializeField] private Button exitGameButton1;

    [Header("Finish level screen")]
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button backMenuButton2;
    [SerializeField] private Button exitGameButton2;

    [Header("Game complete screen")]
    [SerializeField] private GameObject gameCompleteScreen;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button exitGameButton3;
    [SerializeField] private Button backMenuButton3;
    [SerializeField] private int _firstSceneIndex = 1;

    [Header("Pause screen")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button returnToGameButton;
    [SerializeField] private Button backMenuButton4;
    [SerializeField] private Button pauseRestartGameButton;
    [SerializeField] private Button pauseExitGameButton;

    private Player player;
    private PlayerInventory playerInventory;
    private bool allFruitsCollected = false;
    [SerializeField] private int _menuSceneIndex = 0;


    private Dictionary<string, int> totalFruitsOnMap = new Dictionary<string, int>();

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        gameOverScreenObject.SetActive(false);
        player.OnLose += DisplayGameOverScreen;

        levelCompleteScreen.SetActive(false);
        gameCompleteScreen.SetActive(false);
        pauseScreen.SetActive(false);

        gameOverRestartButton.onClick.AddListener(RestartScene);
        nextLevelButton.onClick.AddListener(MoveToNextLevel);
        restartGameButton.onClick.AddListener(RestartGame);
        exitGameButton1.onClick.AddListener(ExitGame);
        exitGameButton2.onClick.AddListener(ExitGame);
        exitGameButton3.onClick.AddListener(ExitGame);
        backMenuButton1.onClick.AddListener(BackToMenu);
        backMenuButton2.onClick.AddListener(BackToMenu);
        backMenuButton3.onClick.AddListener(BackToMenu);
        backMenuButton4.onClick.AddListener(BackToMenu);

        returnToGameButton.onClick.AddListener(TogglePauseScreen);
        pauseRestartGameButton.onClick.AddListener(RestartGame);
        pauseExitGameButton.onClick.AddListener(ExitGame);

        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit fruit in fruits)
        {
            if (!totalFruitsOnMap.ContainsKey(fruit.tag))
            {
                totalFruitsOnMap[fruit.tag] = 0;
            }
            totalFruitsOnMap[fruit.tag]++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseScreen();
        }

        if (!pauseScreen.activeSelf)
        {
            ListOfFruits();
            CheckAllFruitsCollected();
            UpdateLivesDisplay();
        }
    }

    private void TogglePauseScreen()
    {
        if (gameOverScreenObject.activeSelf || levelCompleteScreen.activeSelf || gameCompleteScreen.activeSelf)
        {
            return;
        }

        bool isActive = pauseScreen.activeSelf;
        pauseScreen.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0;
    }

    private void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_menuSceneIndex);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(_firstSceneIndex);
        Time.timeScale = 1;
    }

    private void MoveToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void UpdateLivesDisplay()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = (i < player.lives);
        }
    }

    private void ListOfFruits()
    {
        bananaCounterText.text = $"{playerInventory.GetCollectedFruits("Banana")}/{totalFruitsOnMap["Banana"]}";
        pineappleCounterText.text = $"{playerInventory.GetCollectedFruits("Pineapple")}/{totalFruitsOnMap["Pineapple"]}";
        cherryCounterText.text = $"{playerInventory.GetCollectedFruits("Cherry")}/{totalFruitsOnMap["Cherry"]}";
    }

    private void CheckAllFruitsCollected()
    {
        if (playerInventory.GetCollectedFruits("Banana") == totalFruitsOnMap["Banana"] &&
            playerInventory.GetCollectedFruits("Pineapple") == totalFruitsOnMap["Pineapple"] &&
            playerInventory.GetCollectedFruits("Cherry") == totalFruitsOnMap["Cherry"])
        {
            allFruitsCollected = true;
        }
    }

    public bool AreAllFruitsCollected()
    {
        return allFruitsCollected;
    }

    public void TryDisplayLevelCompleteScreen()
    {
        if (allFruitsCollected)
        {
            player.gameObject.SetActive(false);

            if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
            {
                levelCompleteScreen.SetActive(true);
            }
            else
            {
                gameCompleteScreen.SetActive(true);
            }
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    private void DisplayGameOverScreen()
    {
        gameOverScreenObject.SetActive(true);
    }
}
