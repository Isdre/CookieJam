using DG.Tweening;
using UnityEngine;

public class LookAtAnt : MonoBehaviour
{
    [SerializeField]
    private float lookDuration;

    [SerializeField]
    private Transform watchedObject;

    public void StartLooking()
    {
        var playerHead = GameObject.FindGameObjectWithTag("Player/Head").transform;
        var playerBody = GameObject.FindGameObjectWithTag("Player").transform;

        var direction = watchedObject.position - playerHead.position;
        var flatDirection = new Vector3(direction.x, 0, direction.z);
        float headAngleX = Vector3.SignedAngle(flatDirection, direction, Vector3.Cross(flatDirection, direction));
        float bodyAngleY = Vector3.SignedAngle(Vector3.forward, flatDirection, Vector3.up);

        playerHead.DOLocalRotate(new Vector3(headAngleX, 0, 0), lookDuration)
            .SetEase(Ease.InOutSine);

        playerBody.DOLocalRotate(new Vector3(0, bodyAngleY, 0), lookDuration)
            .SetEase(Ease.InOutSine);

    }
}
