using LevelManagement;
using Narrator;
using System.Collections;
using System.Collections.Generic;
using Forest;
using UnityEngine;

public class DontLeavePathStoryDirector : MonoBehaviour
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
    [SerializeField]
    private NarratorCommentSequence leavingPathNarratorCommentSequence;

    [Header("Good boy")]
    [SerializeField] private float timeToReturn = 3f;
    [SerializeField]
    private NarratorCommentSequence commingBackNarratorCommentSequence;

    private bool goingBack;
    
    [Header("Waiting")] 
    [SerializeField] private float waitingDuration = 40f;
    
    
    private PlayerMovement player;

    private CutTree[] trees;
    private Fireplace[] fireplaces;
    private StepOnIt[] feet;
    private ScreamOnIt[] kids;

    private OnPath path;
    
    private IEnumerator Start()
    {
        NarratorController.Instance.Stop();
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

        path = GetComponentsInChildren<OnPath>()[0];
        path.OnLeavingPath.AddListener(StartLeavingPath);
        path.OnEnteringPath.AddListener(ReturningPath);
        
        MushroomBasket.instance.OnCollectingAllMushroom.AddListener(Win);
        Invoke(nameof(Win), waitingDuration);
        yield return new WaitForSeconds(initialNarratorSequenceDelay);
        initialNarratorSequence.Say();
        yield return new WaitForSeconds(enablePlayerMovementDelay);
        player.enabled = true;
    }

    public void LostOnDestroy()
    {
        CancelInvoke();
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
        NarratorController.OnSequenceEnded += LoseAfterTalking;
        NarratorController.Instance.Say(destroyerNarratorCommentSequence);
    }
    
    public void LostOnLeavePath()
    {
        CancelInvoke();
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
        NarratorController.OnSequenceEnded += LoseAfterTalking;
        NarratorController.Instance.Say(leavingPathNarratorCommentSequence);
    }

    public void StartLeavingPath() {
        goingBack = false;
        NarratorController.Instance.Stop();
        NarratorController.Instance.Say(commingBackNarratorCommentSequence);
        StartCoroutine(StillCanReturn());
    }

    public void ReturningPath()
    {
        goingBack = true;
    }
    
    private IEnumerator StillCanReturn() {
        float t = 0f;
        while (t < timeToReturn) {
            t += Time.deltaTime;
            yield return null;
        }
        if (!goingBack) {
            NarratorController.Instance.Stop();
            LostOnLeavePath();
        }
    }
    
    private void LoseAfterTalking(NarratorCommentSequence sequence)
    {
        NarratorController.OnSequenceEnded -= LoseAfterTalking;
        Lose();
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
