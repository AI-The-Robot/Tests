using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private float _horizontal, _vertical;
    private Vector3 _movDirection;
    [SerializeField] private float attackMarchDistance;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

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
        CalculateDirection();
        RotatePlayer();
        MovePlayer();
    }

    void GetInput(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;
    }

    void CalculateDirection()
    {
        _movDirection = new Vector3(_horizontal, 0,_vertical).normalized;
        
        
    }

    void RotatePlayer()
    {
        if (_movDirection.magnitude >= 0.1f)
        {
            float targetAngle= Mathf.Atan2(_movDirection.x, _movDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle,0);
        }
    }

    void MovePlayer()
    {
        _controller.Move(_movDirection * (speed * Time.deltaTime));
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
