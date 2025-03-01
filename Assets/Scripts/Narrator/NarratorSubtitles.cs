﻿using System.Collections;
using UnityEngine;

namespace Narrator
{
    public class NarratorSubtitles : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text subtitlesDisplay;

        private NarratorComment currentComment;

        private void OnEnable()
        {
            NarratorController.OnCommentSay += NarratorController_OnCommentSay;
            NarratorController.OnCommentStop += NarratorController_OnCommentStop;
        }

        private void NarratorController_OnCommentSay(NarratorComment comment)
        {
            currentComment = comment;
            subtitlesDisplay.SetText(comment.Message);
            subtitlesDisplay.enabled = true;
            subtitlesDisplay.maxVisibleCharacters = 0;
            StartCoroutine(ShowingSubtitlesCo());
        }

        private IEnumerator ShowingSubtitlesCo()
        {
            var wait = new WaitForSeconds(currentComment.LetterDelay);
            for (int i = 0; i < subtitlesDisplay.text.Length; i++)
            {
                yield return wait; 
                subtitlesDisplay.maxVisibleCharacters++;
            }
        }

        private void NarratorController_OnCommentStop()
        {
            StopAllCoroutines();
            currentComment = null;
            subtitlesDisplay.SetText(string.Empty);
            subtitlesDisplay.enabled = false;
        }

        private void OnDisable()
        {
            NarratorController.OnCommentSay -= NarratorController_OnCommentSay;    
            NarratorController.OnCommentStop -= NarratorController_OnCommentStop;
        }
    }
}
