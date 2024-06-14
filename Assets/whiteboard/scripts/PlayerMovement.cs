using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Vector2 moveInput;

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Handle jumping
        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }

        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
