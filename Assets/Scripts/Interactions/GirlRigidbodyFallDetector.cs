using DG.Tweening;
using UnityEngine;

public class GirlRigidbodyFallDetector : MonoBehaviour
{
    [SerializeField] 
    private float squeezeDelay = 0.5f;
    [SerializeField] 
    private float squeezeDuration = 0.5f;
    [SerializeField]
    private Transform body;
    [SerializeField]
    private LayerMask groundLayers;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            var rb = GetComponent<Rigidbody>();

            var scalingTransform = new GameObject("Scaling Point").transform;
            var collisionPoint = collision.contacts[0].point;
            if (Physics.Raycast(collisionPoint + 0.5f * Vector3.up , Vector3.down, out var hit, 1, groundLayers))
            {
                scalingTransform.position = hit.point;
            }
            else
            {
                scalingTransform.position = collisionPoint;
            }

            DOTween.Sequence()
                .AppendInterval(squeezeDelay)
                .AppendCallback(() =>
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    body.parent = scalingTransform;
                })
                .Append(scalingTransform.DOScaleY(0.01f, squeezeDuration));
        }
    }
}
