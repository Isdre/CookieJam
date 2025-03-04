using LevelManagement;
using Narrator;
using System.Collections;
using System.Collections.Generic;
using Forest;
using Shop;
using UnityEngine;

public class DontStealStoryDirector : MonoBehaviour
{
    public static DontStealStoryDirector Instance { get; private set; }

    [Header("Initialization")]
    [SerializeField]
    private float initialNarratorSequenceDelay = 0.5f;
    [SerializeField]
    private NarratorCommentSequence initialNarratorSequence;
    [SerializeField]
    private float enablePlayerMovementDelay = 1f;

    [Header("Ending")]
    [SerializeField]
    private LookAtAnt lookAtShop;

    [Header("Bad boy")]
    [SerializeField]
    private NarratorCommentSequence destroyerNarratorCommentSequence;
    [SerializeField]
    private bool shouldLookAtShopOnLose;

    [Header("Good boy")]
    [SerializeField]
    private NarratorCommentSequence goodBoyNarratorCommentSequence;
    [SerializeField]
    private bool disablePlayerOnWin;

    private PlayerMovement player;

    private StealInteraction[] itemsToSteal;
    private ExitInteraction[] exitDoors;
    private CashInteraction[] cashiers;

    [Header("States")]
    public bool stealAnyItem = false;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
    }

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

    public void Leave()
    {
        if (stealAnyItem)
            LostOnSteal();
        else
            StartSayingWinningCommentSequence();
    }

    public void TakeItem()
    {
        stealAnyItem = true;
    }

    public void Pay()
    {
        stealAnyItem = false;
    }

    public void LostOnSteal()
    {
        foreach (var item in itemsToSteal)
            item.OnInteract.RemoveListener(TakeItem);

        foreach (var door in exitDoors)
            door.OnInteract.RemoveListener(Leave);

        foreach (var c in cashiers)
            c.OnInteract.RemoveListener(Pay);
        player.enabled = false;
        NarratorController.Instance.Stop();
        NarratorController.OnSequenceEnded -= WinAfterTalking;
        NarratorController.OnSequenceEnded += LoseAfterTalking;
        NarratorController.Instance.Say(destroyerNarratorCommentSequence);
        if (shouldLookAtShopOnLose && lookAtShop)
            lookAtShop.StartLooking();
    }

    private void LoseAfterTalking(NarratorCommentSequence sequence)
    {
        NarratorController.OnSequenceEnded -= LoseAfterTalking;
        Lose();
    }

    private void StartSayingWinningCommentSequence()
    {
        if (disablePlayerOnWin)
        {
            player.enabled = false;
            lookAtShop?.StartLooking();
        }

        NarratorController.Instance.Stop();
        NarratorController.OnSequenceEnded += WinAfterTalking;
        NarratorController.Instance.Say(goodBoyNarratorCommentSequence);
    }

    private void WinAfterTalking(NarratorCommentSequence sequence)
    {
        foreach (var item in itemsToSteal)
            item.OnInteract.RemoveListener(TakeItem);

        foreach (var door in exitDoors)
            door.OnInteract.RemoveListener(Leave);

        foreach (var c in cashiers)
            c.OnInteract.RemoveListener(Pay);
        NarratorController.OnSequenceEnded -= WinAfterTalking;
        Win();
    }

    private void Win()
    {
        NarratorController.Instance.Stop();
        LevelsManager.Instance.NextLevel();
    }

    private void Lose()
    {
        NarratorController.Instance.Stop();
        LevelsManager.Instance.NextLevelVariant();
    }
}