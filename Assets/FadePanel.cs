using DG.Tweening;
using LevelManagement;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private float fadeInDuration;
    [SerializeField]
    private float fadeOutDuration;

    private void OnEnable()
    {
        LevelsManager.OnLevelUnloading += FadeIn;
        LevelsManager.OnLevelUnloaded += ForceFaded;
        LevelsManager.OnLevelLoading+= ForceFaded;
        LevelsManager.OnLevelLoaded += FadeOut;
    }

    private void FadeIn()
    {
        image.enabled = true;
        image.DOFade(1, fadeInDuration);
    }

    private void ForceFaded()
    {
        image.enabled = true;
        var color = image.color;
        color.a = 1;
        image.color = color;
    }
    private void FadeOut()
    {
        image.DOFade(0, fadeOutDuration)
            .OnComplete(() => image.enabled = false);
    }

    private void OnDisable()
    {
        LevelsManager.OnLevelUnloading -= FadeIn;
        LevelsManager.OnLevelUnloaded -= ForceFaded;
        LevelsManager.OnLevelLoading -= ForceFaded;
        LevelsManager.OnLevelLoaded -= FadeOut;
    }
}
