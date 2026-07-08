using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StorySlideShowScene : MonoBehaviour
{
    private SlideShowPlayer _slideShowPlayer;

    private void Awake()
    {
        _slideShowPlayer = FindObjectOfType<SlideShowPlayer>();
    }

    private void Start()
    {
        if (_slideShowPlayer != null)
            _slideShowPlayer.OnSlideShowStopped += OnSlideShowStopped;
    }

    private void OnDestroy()
    {
        if (_slideShowPlayer != null)
            _slideShowPlayer.OnSlideShowStopped -= OnSlideShowStopped;
    }

    private void OnSlideShowStopped(object sender, SlideShowPlayerEventArgs e)
    {
        SceneLoader_Async.LoadSceneAsync("Tutorial");
    }
}
