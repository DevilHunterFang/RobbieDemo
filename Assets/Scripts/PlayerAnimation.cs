using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController controller;
    Rigidbody2D rb;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        controller = this.GetComponentInParent<PlayerController>();
        rb = this.GetComponentInParent<Rigidbody2D>();
    }

    private void Update() {
        animator.SetFloat(Animator.StringToHash("speed"),Mathf.Abs(controller.xVelocity));
        animator.SetBool(Animator.StringToHash("isOnGround"),controller.isOnGround);
        animator.SetBool(Animator.StringToHash("isCrouching"),controller.isCrouch);
        animator.SetFloat(Animator.StringToHash("verticalVelocity"),rb.velocity.y);
        animator.SetBool(Animator.StringToHash("isHanging"),controller.isHanging);
    }

    public void StepAudio(){
        AudioManager.PlaySFX(0);
    }

    public void CrouchStepAudio(){
        AudioManager.PlaySFX(1);
    }

}
