using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        GameManager.RegisterDoor(this);
    }

    public void Open(){
        animator.SetTrigger("Open");
        AudioManager.PlaySFX(5);
    }
}
