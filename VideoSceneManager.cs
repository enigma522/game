using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneManager : MonoBehaviour
{ 
    public VideoPlayer videoPlayer; // Reference to Video Player
    public string nextScene;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd; // Trigger when video ends
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene); // Replace with the name of your next scene
    }
}
