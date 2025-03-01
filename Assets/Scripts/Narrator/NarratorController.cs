using Bipolar.Prototyping;
using UnityEngine;

namespace Narrator
{
    public class NarratorController : SceneSingleton<NarratorController>
    {
        public static event System.Action<NarratorComment> OnCommentSay;
        public static event System.Action OnCommentStop;

        [SerializeField]
        private AudioSource audioSource;

        protected NarratorComment currentComment;

        public void Say(NarratorComment comment)
        {
            if (currentComment != null)
                Stop();

            currentComment = comment;
            audioSource.clip = currentComment.Audio;
            audioSource.Play();
            OnCommentSay?.Invoke(comment);
        }

        public void Stop()
        {
            currentComment = null;
            audioSource.Stop();
            OnCommentStop?.Invoke();
        }
    }
}
