using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipCutscene : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private float timeTextFadeIn = 1f;
    private bool _changeScene = true;
    private Coroutine _coroutine;

    public void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(sceneToLoad);
        }
        if (Time.timeSinceLevelLoad >= videoPlayer.length) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
