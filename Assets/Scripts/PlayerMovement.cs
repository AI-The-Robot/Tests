using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _controller;
    private PlayerInputHandler _playerInputHandler;
    private Vector2 _input;
    private float _horizontal, _vertical;
    private Vector3 _movDirection;
    [SerializeField] private float attackMarchDistance;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    
    //Gravity
    [Header("Gravity Settings")]
    private readonly float _gravity = -9.81f;
    private float _velocity;
    [SerializeField] private float gravityMultiplier = 3.0f;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _playerInputHandler.OnMove += GetInput;
        _playerInputHandler.OnAttack += AttackMove;
        _controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        
        CalculateDirection();
        
        if (IsServer)
        {
            MovePlayer(_movDirection);
            RotatePlayer();
            ApplyGravity();
        }
        else if (IsClient && IsOwner)
        {
            MovePlayerServerRpc(_movDirection);
            RotatePlayerServerRpc(_movDirection,_input);
        }
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
    
    void GetInput(Vector2 input)
    {
        _horizontal = input.x;
        _vertical = input.y;
    }

    void CalculateDirection()
    {
        _movDirection = new Vector3(_horizontal, _movDirection.y,_vertical).normalized;
        _input = new Vector2(_horizontal, _vertical);
    }

    void RotatePlayer()
    {
        if (_input.sqrMagnitude >= 0.1f)
        {
            float targetAngle= Mathf.Atan2(_movDirection.x, _movDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle,0);
        }
        
    }
    [ServerRpc]
    void RotatePlayerServerRpc(Vector3 direction,Vector2 input)
    {
        if (input.sqrMagnitude >= 0.1f)
        {
            float targetAngle= Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle,0);
        }
        
    }

    [Rpc(SendTo.Server)]
    void MovePlayerServerRpc(Vector3 movDirection)
    {
       _controller.Move(movDirection * (speed * Time.deltaTime));
    }
    
    void MovePlayer(Vector3 movDirection)
    {
        _controller.Move(movDirection*(speed*Time.deltaTime));
    }

    void AttackMove()
    {
        _controller.Move(transform.forward * (Time.deltaTime * attackMarchDistance));
        Debug.Log($"Player {OwnerClientId} Attacked");
    }

    private void OnDisable()
    {
        _playerInputHandler.OnMove -= GetInput;
        _playerInputHandler.OnAttack -= AttackMove;
    }
}
