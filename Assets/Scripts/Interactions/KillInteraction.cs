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
    private MeshRenderer blood;

    public override void Interact(Interactor interactor)
    {
        OnAnimalKilling?.Invoke();
        blood.gameObject.SetActive(true);
        blood.enabled = true;

        var sequence = DOTween.Sequence()
            .Join(body.DOScaleY(0.01f, 1))
            .Join(blood.transform.DOScale(Vector3.one, 1))          
            .OnComplete(() => OnAnimalKilled?.Invoke());
    }
}
