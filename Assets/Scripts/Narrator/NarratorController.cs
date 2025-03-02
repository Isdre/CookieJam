using Bipolar.Prototyping;
using System.Collections;
using UnityEngine;

namespace Narrator
{
    public class NarratorController : SceneSingleton<NarratorController>
    {
        public static event System.Action<NarratorComment> OnCommentSay;
        public static event System.Action<NarratorCommentSequence> OnSequenceStarted;
        public static event System.Action<NarratorCommentSequence> OnSequenceEnded;

        public static event System.Action OnCommentStop;

        [SerializeField]
        private AudioSource audioSource; 

        protected NarratorCommentSequence currentSequence;

        public void Say(NarratorCommentSequence commentSequence)
        {
            Debug.LogError($"Saying {commentSequence.name} sequence");
            if (currentSequence != null)
                Stop();

            currentSequence = commentSequence;
            StartCoroutine(PlayNarratorAudio());
            OnSequenceStarted?.Invoke(commentSequence);
        }

        private IEnumerator PlayNarratorAudio()
        {
            for (int j = 0; j < currentSequence.Comments.Count; j++)
            {
                var comment = currentSequence.Comments[j];
                audioSource.clip = comment.Audio;
                audioSource.Play();
                OnCommentSay?.Invoke(comment);
                yield return new WaitWhile(() => audioSource.isPlaying);
                yield return new WaitForSeconds(comment.DelayAfter);
            }

            OnSequenceEnded?.Invoke(currentSequence);
            Stop();
        }

        [ContextMenu("Stop")]
        public void Stop()
        {
            currentSequence = null;
            StopAllCoroutines();
            audioSource.Stop();
            OnCommentStop?.Invoke();
        }

        public static void ClearEvents()
        {
            OnSequenceStarted = null;
            OnSequenceEnded = null;
        }
    }
}
