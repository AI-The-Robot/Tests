using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkInputManger : NetworkBehaviour
{
    
    public PlayerInput PlayerInput; 
    private PlayerInputHandler _playerInputHandler;
    private PlayerMovement _playerMovement;
    private CharacterController _characterController;
    private void OnEnable()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _playerMovement = GetComponent<PlayerMovement>();
        _characterController = GetComponent<CharacterController>();
        _playerInputHandler.enabled = false;
        _playerMovement.enabled = false;
        _characterController.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;
        if (!IsOwner)
        {
            enabled = false;
            _playerInputHandler.enabled = false;
            _playerMovement.enabled = false;
            _characterController.enabled = false;
            return;
        }
        
        _playerInputHandler.enabled = true;
        _playerMovement.enabled = true;
        _characterController.enabled = true;
    }

    
}
