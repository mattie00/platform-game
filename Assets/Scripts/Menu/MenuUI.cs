using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button startGmeButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private int firtsGameplaySceneIndex = 1;

    private void Start()
    {
        startGmeButton.onClick.AddListener(StartGame);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(firtsGameplaySceneIndex);
    }
}
