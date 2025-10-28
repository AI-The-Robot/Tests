using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float speed;

    private float _horizontalInput;
    private float _verticalInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(_horizontalInput, 0, _verticalInput);
        
        _controller.Move(movement * (speed * Time.deltaTime));
    }
}