using DG.Tweening;
using UnityEngine;

public abstract class DeathSequence : MonoBehaviour
{
    public event System.Action OnSequenceFinished;

    public abstract void Play();

    protected void Completed() => OnSequenceFinished?.Invoke();
}

public class SqueezeDeathSequence : DeathSequence
{
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

    public override void Play()
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
            Completed();
        });
    }

}
