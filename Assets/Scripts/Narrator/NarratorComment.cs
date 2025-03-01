using UnityEngine;

namespace Narrator
{
    [CreateAssetMenu]
    public class NarratorComment : ScriptableObject
    {
        [SerializeField]
        private AudioClip audio;
        public AudioClip Audio => audio;

        [Header("Subtitles")]
        [SerializeField]
        private string message;
        public string Message => message;

        [SerializeField]
        private float delayPerLetter;
        public float LetterDelay => delayPerLetter;

        [ContextMenu("Say")]
        public void Say()
        {
            NarratorController.Instance.Say(this);
        }
    }
}
