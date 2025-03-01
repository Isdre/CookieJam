using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NarratorComment
{
    [SerializeField]
    private AudioClip audio;
    public AudioClip Audio => audio;

    [SerializeField]
    private float delayAfter = 1;
    public float DelayAfter => delayAfter;  

    [Header("Subtitles")]
    [SerializeField]
    private string message;
    public string Message => message;

    [SerializeField]
    private float delayPerLetter = 0.05f;
    public float LetterDelay => delayPerLetter;

    public NarratorComment(AudioClip audio, string message)
    {
        this.audio = audio;
        this.message = message;
    }
}