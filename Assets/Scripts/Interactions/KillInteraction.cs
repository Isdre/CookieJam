using Bipolar.InteractionSystem;
using Bipolar.RaycastSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class KillInteraction : Interaction
{
    public static event System.Action OnAnimalKilling;
    public static event System.Action OnAnimalKilled;

    [SerializeField, FormerlySerializedAs("squeezeDeathSequence")]
    private DeathSequence deathSequence;

    [SerializeField]
    private RaycastTarget raycastTargetToDisable;

    public override void Interact(Interactor interactor)
    {
        OnAnimalKilling?.Invoke();
        if (raycastTargetToDisable)
            raycastTargetToDisable.enabled = false;
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
