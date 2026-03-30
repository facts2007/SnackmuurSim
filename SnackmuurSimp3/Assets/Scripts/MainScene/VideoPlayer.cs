using UnityEngine;
using UnityEngine.Video;

public class ShowStartButtonAfterVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject startButton;

    void Start()
    {
        startButton.SetActive(false);
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        vp.Pause();
        startButton.SetActive(true);
    }
}