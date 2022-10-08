using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject DeathVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            Instantiate(DeathVFX, this.transform.position, this.transform.localRotation);
            AudioManager.PlaySFX(3);
            this.gameObject.SetActive(false);
            GameManager.PlayerDied();
        }
    }
}
