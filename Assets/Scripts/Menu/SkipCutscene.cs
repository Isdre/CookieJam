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
    private bool _changeScene;
    private Coroutine _coroutine;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(_changeScene) {
                StopCoroutine(_coroutine);
                SceneManager.LoadScene(sceneToLoad);
                }
            else {
                _changeScene = true;
                _coroutine = StartCoroutine(FadeInText());
            }
        }
        if (Time.timeSinceLevelLoad >= videoPlayer.length) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private IEnumerator FadeInText() {
        float elapsedTime = 0f;
        while (elapsedTime < timeTextFadeIn) {
            elapsedTime += Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, elapsedTime / timeTextFadeIn);
            yield return null;
        }
    }
}
