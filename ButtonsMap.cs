using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ButtonsMap : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void LoadScene(string nextScene) // This function will be called on button click
    {
        if (!string.IsNullOrEmpty(nextScene)) // Ensure a scene name is set
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
