using Bipolar.InteractionSystem;
using Bipolar.RaycastSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class KillInteraction : Interaction
{
    public static event System.Action OnAnimalKilling;
    public static event System.Action OnAnimalKilled;

    [SerializeField, FormerlySerializedAs("squeezeDeathSequence")]
    private DeathSequence deathSequence;

    [SerializeField]
    private RaycastTarget raycastTargetToDisable;

    public UnityEvent OnInteract;

    public override void Interact(Interactor interactor)
    {
        OnAnimalKilling?.Invoke();
        if (raycastTargetToDisable)
            raycastTargetToDisable.enabled = false;
        OnInteract.Invoke();
        if (deathSequence)
        {
            deathSequence.OnSequenceFinished += DeathSequence_OnSequenceFinished;
            deathSequence.Play();
        }
    }

    private void DeathSequence_OnSequenceFinished()
    {
        if (deathSequence)
            deathSequence.OnSequenceFinished -= DeathSequence_OnSequenceFinished;
        OnAnimalKilled?.Invoke();
    }
}
