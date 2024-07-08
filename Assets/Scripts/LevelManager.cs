using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; // This is useful when we have a simple game. One object is needed to coordinate actions across the system. In this case, we are using it to manage the scenes in the game.
    private void Awake()
    {
        Instance = this;
    }

    // enum is a data type that allows for a variable to be one of a set of predefined constants. Enums can represent diffferent states or modes within a game.
    // In this case, MechanicsTesting would be scene 0, Credits scene would be scene 1 and MainMenu scene 2.
    public enum Scene
    {
        MechanicsTesting,
        Credits,
        MainMenu,
    }

    // A general function to load a particular scene.
    public void LoadScene(Scene scene)
    {         
        SceneManager.LoadScene(scene.ToString()); // Passing it as a string so we can read it in the load scene.
    }

    // Load up Jared's testing scene.
    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.MechanicsTesting.ToString());
    }

    // Load up the credits scene.
    public void LoadCredits()
    {
        SceneManager.LoadScene(Scene.Credits.ToString());
    }

    // Load up the main menu.
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
