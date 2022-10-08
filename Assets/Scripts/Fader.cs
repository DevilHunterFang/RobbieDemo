using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        GameManager.RegisterFader(this);
    }

    public void setTrigger(){
        animator.SetTrigger(Animator.StringToHash("Fade"));
    }
}
