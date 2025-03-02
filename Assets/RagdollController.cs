using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] rigidbodies;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRigidbodiesKinematic(!enabled);
    }   

    private void OnEnable()
    {
        SetRigidbodiesKinematic(false);
    }

    private void OnDisable()
    {
        SetRigidbodiesKinematic(true);
    }

    private void SetRigidbodiesKinematic(bool kinematic)
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = kinematic;
        }
    }
}
