using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : NetworkBehaviour
{
    
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _attackAction;
    public event Action<Vector2> OnMove;
    public event Action OnAttack;
    
    void OnEnable()
    { 
      _playerInput = new PlayerInput();
      _playerInput.Enable();
      _moveAction = _playerInput.Player.Move;
      _attackAction = _playerInput.Player.Attack;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsOwner)
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _moveAction = _playerInput.Player.Move;
            _attackAction = _playerInput.Player.Attack;

            _moveAction.performed += context => OnMove?.Invoke(context.ReadValue<Vector2>());
            _moveAction.canceled += context => OnMove?.Invoke(Vector2.zero);
            _attackAction.performed += context => OnAttack?.Invoke();
        }
    }
    void OnDisable()
    {
        _playerInput.Disable();
    }
}
