using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private float _speed = 4f, _Xspeed = 6f, _gravity = -9.8f, _jump = 1f, staminaConsumptionRate = 12.5f, staminaRegenerationRate = 10f;

    private float currentSpeed;
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;

    [Header("Gravity and Direction")]
    Vector3 _velocity;
    private bool isGrounded;
    void Start()
    {
        currentSpeed = _speed;
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && PlayerStats.stamina > 0)
        {
            currentSpeed = _Xspeed;
            PlayerStats.stamina -= Time.deltaTime * staminaConsumptionRate;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && PlayerStats.stamina <= 0)
        {
            currentSpeed = _speed;
        }
        else
        {
            currentSpeed = _speed;
            if (PlayerStats.stamina <= PlayerStats.staminaLimit)
            {
                PlayerStats.stamina += Time.deltaTime * staminaRegenerationRate;
            }
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        characterController.Move(_velocity * Time.deltaTime);
    }
}
