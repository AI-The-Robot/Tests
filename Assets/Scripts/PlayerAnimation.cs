using System;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAnimation : MonoBehaviour
{
   PlayerInputHandler _playerInputHandler;
   public Animator animator;
   public VisualEffect effect;
   public Transform VfxTransform;
   bool isRight;

   void Awake()
   {
      _playerInputHandler = GetComponent<PlayerInputHandler>();
      _playerInputHandler.OnAttack += Swing;
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
      if (isRight) VfxTransform.localScale = new Vector3(-1, 1, 1);
      else VfxTransform.localScale = new Vector3(1, 1, 1);
      effect?.Play();
   }

   private void OnDisable()
   {
      _playerInputHandler.OnAttack-= Swing;
   }
}
