using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoGoodPlayer;
    [SerializeField] private VideoPlayer videoBadPlayer;
    private VideoPlayer videoPlayer;

    [SerializeField] private List<string> levels = new();

    private bool badEnding = false;

    private void Start() {
        foreach(string l in levels){
            int i = PlayerPrefs.GetInt(l, 0);
            if (i != 0) {
                badEnding = true;
                break;
            }
        }

        if (badEnding)
            videoPlayer = videoBadPlayer;
        else
            videoPlayer = videoGoodPlayer;

        videoPlayer.Play();
    }

    private void Update() {
        if (Time.timeSinceLevelLoad >= videoPlayer.length)
            Application.Quit();
    }
}