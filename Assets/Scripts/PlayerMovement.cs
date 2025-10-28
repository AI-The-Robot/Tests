using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private float _horizontal, _vertical;
    private Vector3 _movDirection;
    [SerializeField] private float attackMarchDistance;
    [SerializeField] private float speed;

    private void Awake()
    {
        PlayerInput.OnMove += GetInput;
        PlayerInput.OnAttack += AttackMove;
    }
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        CalculateDirection();
        MovePlayer();
    }

    void GetInput(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;
    }

    void CalculateDirection()
    {
        Vector2 direction = new Vector2(_horizontal, _vertical);
        _movDirection = new Vector3(direction.x, 0, direction.y).normalized;
        
    }

    void RotatePlayer()
    {
        
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
