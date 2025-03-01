using DG.Tweening;
using UnityEngine;

public class SqueezeDeathSequence : MonoBehaviour
{
    public event System.Action OnSequenceFinished;

    [SerializeField]
    private float duration = 0.5f;

    [Header("Body")]
    [SerializeField]
    private Transform squeezedBody;
    public Transform SqueezedBody
    {
        get => squeezedBody;
        set => squeezedBody = value;
    }

    [Header("Blood")]
    [SerializeField]
    private MeshRenderer blood;
    [SerializeField]
    private Vector3 bloodScale = Vector3.one;
    [SerializeField]
    private ParticleSystem bloodParticles;

    public void Squeeze()
    {
        if (blood)
        {
            blood.gameObject.SetActive(true);
            blood.enabled = true;
        }

        bloodParticles?.Play();
        var sequence = DOTween.Sequence()
            .Join(squeezedBody.DOScaleY(0.01f, duration))
            .Join(blood?.transform.DOScale(bloodScale, duration))
                .OnComplete(() =>
        {
            bloodParticles?.Stop();
            OnSequenceFinished?.Invoke();
        });
    }

}
