using LevelManagement;
using Narrator;
using System.Collections;
using System.Collections.Generic;
using Forest;
using UnityEngine;

public class DontDisturbStoryDirector : MonoBehaviour
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

    private CutTree[] trees;
    private Fireplace[] fireplaces;
    private StepOnIt[] feet;
    private ScreamOnIt[] kids;
    
    private IEnumerator Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        trees = GetComponentsInChildren<CutTree>();
        fireplaces = GetComponentsInChildren<Fireplace>();
        feet = GetComponentsInChildren<StepOnIt>();
        kids = GetComponentsInChildren<ScreamOnIt>();
        
        foreach (var t in trees) 
            t.OnInteract.AddListener(LostOnDestroy);
        
        foreach (var t in fireplaces) 
            t.OnInteract.AddListener(LostOnDestroy);
        
        foreach (var t in feet) 
            t.OnInteract.AddListener(LostOnDestroy);
        
        foreach (var t in kids) 
            t.OnInteract.AddListener(LostOnDestroy);
        
        MushroomBasket.instance.OnCollectingAllMushroom.AddListener(StartSayingWinningCommentSequence);
        
        yield return new WaitForSeconds(initialNarratorSequenceDelay);
        initialNarratorSequence.Say();
        yield return new WaitForSeconds(enablePlayerMovementDelay);
        player.enabled = true;
    }

    public void LostOnDestroy()
    {
        foreach (var t in trees) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        
        foreach (var t in fireplaces) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        
        foreach (var t in feet) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        
        foreach (var t in kids) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        
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
        foreach (var t in trees) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        
        foreach (var t in fireplaces) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        
        foreach (var t in feet) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        
        foreach (var t in kids) 
            t.OnInteract.RemoveListener(LostOnDestroy);
        LevelsManager.Instance.NextLevelVariant();
    }

    private void Lose()
    {
        LevelsManager.Instance.NextLevelVariant();
    }
}