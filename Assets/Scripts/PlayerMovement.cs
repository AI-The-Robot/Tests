using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private Vector2 _input;
    private float _horizontal, _vertical;
    private Vector3 _movDirection;
    [SerializeField] private float attackMarchDistance;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    //Gravity
    [Header("Gravity Settings")]
    private readonly float _gravity = -9.81f;
    private float _velocity;
    [SerializeField] private float gravityMultiplier = 3.0f;

    private void Awake()
    {
        PlayerInput.OnMove += GetInput;
    }
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        ApplyGravity();
        CalculateDirection();
        RotatePlayer();
        MovePlayer();
    }

    void ApplyGravity()
    {
        if (_controller.isGrounded &&  _velocity < 0.0f)
        {
            _velocity = -1f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
        
        _movDirection.y = _velocity;
    }
    
    void GetInput(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;
    }

    void CalculateDirection()
    {
        _movDirection = new Vector3(_horizontal, _movDirection.y,_vertical).normalized;
        _input = new Vector2(_horizontal, _vertical);
    }

    void RotatePlayer()
    {
        if (_input.sqrMagnitude == 0f) return; 
        float targetAngle= Mathf.Atan2(_movDirection.x, _movDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle,0);
    }

    void MovePlayer()
    {
        if (_movDirection.magnitude >= 0.1f)
        {
            _controller.Move(_movDirection * (speed * Time.deltaTime));
        }
    }

    void AttackMove()
    {
        _controller.Move(gameObject.transform.forward * (Time.deltaTime * attackMarchDistance));
    }

    private void OnDisable()
    {
        PlayerInput.OnMove -= GetInput;
        PlayerInput.OnAttack -= AttackMove;
    }
}
