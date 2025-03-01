using DG.Tweening;
using UnityEngine;

public class RollDeathSequence : DeathSequence
{
    [SerializeField]
    private float sequenceDuration = 2;

    [Header("Rolling")]
    [SerializeField]
    private Transform rotatingBody;
    [SerializeField]
    private float rotationSpeed = 10;

    [Header("Bleeding")]
    [SerializeField]
    private MeshRenderer bodyRenderer;
    [SerializeField]
    private float colorizingDuration = 1.5f;
    [SerializeField]
    private Color bloodyColor;
    [SerializeField]
    private ParticleSystem bloodParticles;

    private bool isRotating;

    public override void Play()
    {
        Invoke(nameof(Completed), sequenceDuration);
        isRotating = true;
        bloodParticles?.Play();
        bodyRenderer.material.DOColor(bloodyColor, colorizingDuration); 
    }

    private void Update()
    {
        if (isRotating)
        {
            rotatingBody.Rotate(rotatingBody.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
