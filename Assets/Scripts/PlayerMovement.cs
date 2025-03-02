using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private Transform head;
    [SerializeField]
    private float moveSpeed = 4;
    [SerializeField]
    private Vector2 cameraSensitivity;
    [SerializeField]
    private LayerMask groundLayers;

    private float headPitch;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        headPitch = head.localEulerAngles.x;
    }

    private void Update()
    {
        HandleRotation();
        MovePlayer();
    }

    public void ResetHead()
    {
        headPitch = 0;
        head.localRotation = Quaternion.identity;
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        headPitch -= mouseY * cameraSensitivity.y;
        headPitch = Mathf.Clamp(headPitch, -90, 90);
        head.localRotation = Quaternion.AngleAxis(headPitch, Vector3.right);

        float bodyYaw = mouseX * cameraSensitivity.x;
        transform.Rotate(Vector3.up, bodyYaw);
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        direction.y = 0;

        characterController.SimpleMove(moveSpeed * direction);
    }
}
