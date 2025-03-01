using Bipolar.InteractionSystem;
using DG.Tweening;
using UnityEngine;

public class KillInteraction : Interaction
{
    public static event System.Action OnAnimalKilling;
    public static event System.Action OnAnimalKilled;

    [SerializeField]
    private Transform body;
    [SerializeField]
    private Projector bloodDecal;

    public override void Interact(Interactor interactor)
    {
        OnAnimalKilling?.Invoke();
        body.DOScaleY(0.01f, 1)
            .OnComplete(() => OnAnimalKilled?.Invoke());
    }
}
