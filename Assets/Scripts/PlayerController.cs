using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    public float acceleration = 1;
    public float maxSpeed = 10;
    public float lookSpeed = 90;
    private float frictionCoefficient = 0.5f;
    public bool active = true;
    public event Action PlayerFired;

    void HandlePlayerMovement()
    {
        // Determine translation
        float vInput = Input.GetAxisRaw("Vertical");
        float hInput = Input.GetAxisRaw("Horizontal");

        // Determine input acceleration
        Vector3 acc = acceleration * new Vector3(hInput, 0, vInput).normalized;

        // Factor in frictional acceleration
        acc += (velocity * -1).normalized * frictionCoefficient;

        // Determine the new velocity and translate position, enforce max speed by clipping velocity
        velocity += acc * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            // Velocity is set to a vector with maxSpeed magnitude in the same direction as the original velocity
            velocity = velocity.normalized * maxSpeed;
        }
        transform.Translate(velocity);
    }

    void HandlePlayerRotation()
    {
        // Determine player rotation
        float rInput = Input.GetAxisRaw("Mouse X");
        transform.Rotate(rInput * lookSpeed * Time.deltaTime * Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Cursor.lockState == CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
        }
#else
#endif
        if (!active)
        {
            return;
        }
        HandlePlayerMovement();
        HandlePlayerRotation();
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetAxisRaw("Fire1") <= 0.05)
        {
            return;
        }
        PlayerFired();

    }

}
