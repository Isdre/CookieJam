using DG.Tweening;
using LevelManagement;
using Narrator;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class DelayedEvent
{
    public float delay;
    public UnityEngine.Events.UnityEvent action;

    public void CallDelayed()
    {
        DOTween.Sequence()
            .AppendInterval(delay)
            .AppendCallback(() => action.Invoke());
    }
}

public class DontKillStoryDirector : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField]
    private float initialNarratorSequenceDelay = 0.5f;
    [SerializeField]
    private NarratorCommentSequence initialNarratorSequence;
    [SerializeField]
    private float enablePlayerMovementDelay = 1f;
    [SerializeField]
    private DelayedEvent[] initializationEvents;

    [Header("Killing")]
    [SerializeField]
    private NarratorCommentSequence killNarratorSequence;

    [Header("Waiting")]
    [SerializeField]
    private float waitingDuration = 10f;
    [SerializeField]
    private NarratorCommentSequence afterWaitNarratorSequence;

    private PlayerMovement player;

    private IEnumerator Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        KillInteraction.OnAnimalKilling += KillInteraction_OnAnimalKilling;

        foreach (var delayedEvent in initializationEvents)
            delayedEvent.CallDelayed();

        yield return new WaitForSeconds(initialNarratorSequenceDelay);
        NarratorController.OnSequenceEnded += StartWaitingForWinningSequence;
        NarratorController.Instance.Say(initialNarratorSequence);
        yield return new WaitForSeconds(enablePlayerMovementDelay);
        player.enabled = true;
    }

    private void StartWaitingForWinningSequence(NarratorCommentSequence sequence)
    {
        NarratorController.OnSequenceEnded -= StartWaitingForWinningSequence;
        Invoke(nameof(StartSayingWinningCommentSequence), waitingDuration);
    }

    private void KillInteraction_OnAnimalKilling()
    {
        KillInteraction.OnAnimalKilling -= KillInteraction_OnAnimalKilling;
        NarratorController.OnSequenceEnded -= StartWaitingForWinningSequence;
        player.enabled = false;
        CancelInvoke();
        NarratorController.Instance.Stop();
        KillInteraction.OnAnimalKilled += KillInteraction_OnAnimalKilled;
        NarratorController.OnSequenceEnded -= WinAfterTalking;
        NarratorController.OnSequenceEnded += LoseAfterTalking;
        NarratorController.Instance.Say(killNarratorSequence);
    }

    private void KillInteraction_OnAnimalKilled()
    {
        KillInteraction.OnAnimalKilled -= KillInteraction_OnAnimalKilled;
        player.enabled = true;  
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
