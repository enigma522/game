using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenucs : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("StartScene"); 
    }
}
