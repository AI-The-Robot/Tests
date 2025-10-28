using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    float _horizontal;
    float _vertical;
    
    public static event Action<float,float> OnMove;


    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        OnMove?.Invoke(_horizontal, _vertical);
    }
}
