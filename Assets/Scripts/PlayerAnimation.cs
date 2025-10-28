using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
   public Animator animator;
   bool isRight;

   void Awake()
   {
      PlayerInput.OnAttack += Swing;
      isRight = true;
   }

   private void Update()
   {
      animator.SetBool("IsRight", isRight);
   }

   void Swing()
   {
      
      animator.SetTrigger("Swing");
      isRight = !isRight;
      
   }

   private void OnDisable()
   {
      PlayerInput.OnAttack-= Swing;
   }
}
