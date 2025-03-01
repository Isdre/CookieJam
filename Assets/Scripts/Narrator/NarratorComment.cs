using Unity.VisualScripting;
using UnityEngine;

namespace Narrator
{
    [CreateAssetMenu]
    public class NarratorComment : ScriptableObject
    {
        [SerializeField]
        private string message;
        public string Message => message;

        [SerializeField]
        private float delayPerLetter;
        public float LetterDelay => delayPerLetter;

        [SerializeField]
        private AudioClip audio;
        public AudioClip Audio => audio;

        [ContextMenu("Say")]
        private void SayInEditor()
        {
            this.Say();
        }
    }

    public static class NarratorCommentExtensions
    {
        public static void Say(this NarratorComment comment)
        {
            if (comment != null)
            {
                NarratorController.Instance.Say(comment);
            }
        }
    }
}
