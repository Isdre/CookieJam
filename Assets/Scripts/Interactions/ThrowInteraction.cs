using Bipolar.InteractionSystem;
using UnityEngine;

public class ThrowInteraction : Interaction
{
    [SerializeField]
    private Rigidbody fallingBody;
    [SerializeField]
    private float pushForce = 10;

    public override void Interact(Interactor interactor)
    {
        fallingBody.isKinematic = false;
        fallingBody.useGravity = true;  
        fallingBody.AddRelativeForce(Vector3.forward * pushForce, ForceMode.Impulse);
    }


}
