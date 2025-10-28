using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    float _horizontal;
    float _vertical;
    
    public static event Action<float,float> OnMove;
    public static event Action OnAttack;

   


    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        OnMove?.Invoke(_horizontal, _vertical); 
        if (Input.GetButtonDown("Fire1")) OnAttack?.Invoke();
    }
}
