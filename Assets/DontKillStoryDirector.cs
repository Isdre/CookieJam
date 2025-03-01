using LevelManagement;
using Narrator;
using System;
using System.Collections;
using UnityEngine;

public class DontKillStoryDirector : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField]
    private float initialNarratorSequenceDelay = 0.5f;
    [SerializeField]
    private NarratorCommentSequence initialNarratorSequence;
    [SerializeField]
    private float enablePlayerMovementDelay = 1f;

    [Header("Killing")]
    [SerializeField]
    private NarratorCommentSequence killNarratorSequence;

    [Header("Waiting")]
    [SerializeField]
    private float waitingDuration = 10f;
    [SerializeField]
    private NarratorCommentSequence afterWaitNarratorSequence;

    private IEnumerator Start()
    {
        var player = GameObject.FindWithTag("Player");
        KillInteraction.OnAnimalKilled += KillInteraction_OnAnimalKilled;
        yield return new WaitForSeconds(initialNarratorSequenceDelay);
        initialNarratorSequence.Say();
        yield return new WaitForSeconds(enablePlayerMovementDelay);
        player.GetComponent<PlayerMovement>().enabled = true;
        Invoke(nameof(StartSayingWinningCommentSequence), waitingDuration);
    }

    private void KillInteraction_OnAnimalKilled()
    {
        KillInteraction.OnAnimalKilled -= KillInteraction_OnAnimalKilled;
        CancelInvoke();
        NarratorController.Instance.Stop();
        NarratorController.OnSequenceEnded -= WinAfterTalking;
        NarratorController.OnSequenceEnded += LoseAfterTalking;
        NarratorController.Instance.Say(killNarratorSequence);
    }

    private void LoseAfterTalking(NarratorCommentSequence sequence)
    {
        NarratorController.OnSequenceEnded -= LoseAfterTalking;
        Lose();
    }

    private void StartSayingWinningCommentSequence()
    {
        NarratorController.Instance.Stop();
        NarratorController.OnSequenceEnded += WinAfterTalking;
        NarratorController.Instance.Say(afterWaitNarratorSequence);
    }

    private void WinAfterTalking(NarratorCommentSequence sequence)
    {
        NarratorController.OnSequenceEnded -= WinAfterTalking;
        Win();
    }

    private void Win()
    {
        LevelsManager.Instance.NextLevelVariant();
    }

    private void Lose()
    {
        LevelsManager.Instance.NextLevelVariant();
    }
}
