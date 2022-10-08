using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public GameObject gainVFX;
    private void Start() {
        GameManager.RegisterOrb(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            Instantiate(gainVFX, this.transform.position, this.transform.localRotation);
            AudioManager.PlaySFX(4);
            this.gameObject.SetActive(false);
            GameManager.GainOrb(this);
        }
    }
}
