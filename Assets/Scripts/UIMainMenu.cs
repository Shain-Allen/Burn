// Main menu related stuff.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    // Declaring the buttons.
    [SerializeField] Button _newGame;
    [SerializeField] Button _Credits;
    [SerializeField] Button _QuitGame;
    [SerializeField] Button _Back;

    // Adding listeners to the buttons.
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        _newGame.onClick.AddListener(StartNewGame);
        _Credits.onClick.AddListener(LoadCredits);
        _QuitGame.onClick.AddListener(QuitGame);
        _Back.onClick.AddListener(BackToMainMenu);
    }

    // Starting a new game.
    private void StartNewGame()
    {
        SceneManager.LoadScene("IntroCutscene");
        //Debug.Log("MechanicsTesting scene has been loaded!");
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
        //Debug.Log("Credits scene has been loaded!");
    }

    private void QuitGame()
    {
        Application.Quit();
        //Debug.Log("Noo, don't close the game!!!");
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //Debug.Log("Main menu scene has been loaded!");
    }
}
