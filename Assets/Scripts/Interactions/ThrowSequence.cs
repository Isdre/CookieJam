using Bipolar.InteractionSystem;
using UnityEngine;

public class ThrowSequence : DeathSequence
{
    [SerializeField]
    private Rigidbody fallingBody;
    [SerializeField]
    private float pushForce;

    [SerializeField]
    private RagdollController ragdollController;

    [SerializeField]
    private ParticleSystem bloodParticles;

    public override void Play()
    {
        ragdollController.enabled = true;
        fallingBody.isKinematic = false;
        fallingBody.useGravity = true;
        
        var direction = Camera.main.transform.forward;
        fallingBody.AddForce((direction + transform.forward) * pushForce, ForceMode.Impulse);

        if (bloodParticles.isPlaying == false)
            bloodParticles.Play();
        
        Completed();
    }
}
