using Bipolar.InteractionSystem;
using UnityEngine;

public class ThrowInteraction : Interaction
{
    [SerializeField]
    private Rigidbody fallingBody;
    [SerializeField]
    private Vector3 pushForce;
    [SerializeField]
    private Vector3 pushTorque;

    public override void Interact(Interactor interactor)
    {
        fallingBody.isKinematic = false;
        fallingBody.useGravity = true;  
        fallingBody.AddRelativeForce(pushForce, ForceMode.Impulse);
        fallingBody.AddRelativeTorque(pushTorque, ForceMode.Impulse);
    }
}
