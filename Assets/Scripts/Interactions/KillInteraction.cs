using Bipolar.InteractionSystem;
using Bipolar.RaycastSystem;
using DG.Tweening;
using UnityEngine;

public class KillInteraction : Interaction
{
    public static event System.Action OnAnimalKilling;
    public static event System.Action OnAnimalKilled;

    [SerializeField]
    private SqueezeDeathSequence squeezeDeathSequence;

    [SerializeField]
    private RaycastTarget raycastTargetToDisable;

    public override void Interact(Interactor interactor)
    {
        OnAnimalKilling?.Invoke();
        if (raycastTargetToDisable)
            raycastTargetToDisable.enabled = false;

        squeezeDeathSequence.OnSequenceFinished += SqueezeDeathSequence_OnSequenceFinished;
        squeezeDeathSequence.Squeeze();
    }

    private void SqueezeDeathSequence_OnSequenceFinished()
    {
        squeezeDeathSequence.OnSequenceFinished -= SqueezeDeathSequence_OnSequenceFinished;
        OnAnimalKilled?.Invoke();
    }
}
