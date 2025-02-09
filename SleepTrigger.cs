using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepTrigger : MonoBehaviour
{
    void Update()
    {
        // Check if the player presses "S"
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Load the next scene (replace "DreamScene" with your actual scene name)
            SceneManager.LoadScene("transition");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("map");
        }
    }
}
