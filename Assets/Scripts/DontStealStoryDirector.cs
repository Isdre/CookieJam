using LevelManagement;
using Narrator;
using System.Collections;
using System.Collections.Generic;
using Forest;
using Shop;
using UnityEngine;

public class DontStealStoryDirector : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField]
    private float initialNarratorSequenceDelay = 0.5f;
    [SerializeField]
    private NarratorCommentSequence initialNarratorSequence;
    [SerializeField]
    private float enablePlayerMovementDelay = 1f;

    [Header("Bad boy")]
    [SerializeField]
    private NarratorCommentSequence destroyerNarratorCommentSequence;

    [Header("Good boy")]
    [SerializeField]
    private NarratorCommentSequence goodBoyNarratorCommentSequence;

    private PlayerMovement player;

    private StealInteraction[] itemsToSteal;
    private ExitInteraction[] exitDoors;
    private CashInteraction[] cashiers;

    private bool stealAnyItem = false;
    
    private IEnumerator Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        
        itemsToSteal = GetComponentsInChildren<StealInteraction>();
        exitDoors = GetComponentsInChildren<ExitInteraction>();
        cashiers = GetComponentsInChildren<CashInteraction>();
        
        foreach (var item in itemsToSteal)
            item.OnInteract.AddListener(TakeItem);
        
        foreach (var door in exitDoors)
            door.OnInteract.AddListener(Leave);
        
        foreach (var c in cashiers)
            c.OnInteract.AddListener(Pay);
        
        yield return new WaitForSeconds(initialNarratorSequenceDelay);
        initialNarratorSequence.Say();
        yield return new WaitForSeconds(enablePlayerMovementDelay);
        player.enabled = true;
    }

    public void Leave() {
        if (stealAnyItem) LostOnSteal();
        else StartSayingWinningCommentSequence();
    }
    
    public void TakeItem() {
        stealAnyItem = true;
    }

    public void Pay() {
        stealAnyItem = false;
    }
    
    public void LostOnSteal() {
        
        player.enabled = false;
        NarratorController.Instance.Stop();
        NarratorController.OnSequenceEnded -= WinAfterTalking;
        NarratorController.OnSequenceEnded += LoseAfterTalking;
        NarratorController.Instance.Say(destroyerNarratorCommentSequence);
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
        NarratorController.Instance.Say(goodBoyNarratorCommentSequence);
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