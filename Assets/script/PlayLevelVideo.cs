using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayLevelVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName); // Langsung pindah
    }
}
