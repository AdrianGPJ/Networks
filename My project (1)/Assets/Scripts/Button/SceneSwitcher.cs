using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import the SceneManagement namespace

public class SceneSwitcher : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void SwitchToClientScene()
    {
        // Load the scene named "Excercise 1_Client"
        SceneManager.LoadScene("Exercise1_Client");
    }

    public void SwitchToServerScene()
    {
        // Load the scene named "Excercise 1_Server"
        SceneManager.LoadScene("Exercise1_Server");
    }
}