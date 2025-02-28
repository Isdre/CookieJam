using System;
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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleRotation();
        MovePlayer();
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float headPitch = -mouseY * cameraSensitivity.y;
        float bodyYaw = mouseX * cameraSensitivity.x;

        head.Rotate(Vector3.right, headPitch);
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
