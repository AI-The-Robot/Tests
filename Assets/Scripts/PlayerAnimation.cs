using System;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAnimation : MonoBehaviour
{
   public Animator animator;
   public VisualEffect  effect;
   private float SlashRotationSpeed= 20;
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

   void PlaySlash()
   {
      if(isRight) effect.SetFloat("Rotation Velocity",SlashRotationSpeed);
      else effect.SetFloat("Rotation Velocity", -SlashRotationSpeed);
      effect?.Play();
   }

   private void OnDisable()
   {
      PlayerInput.OnAttack-= Swing;
   }
}
