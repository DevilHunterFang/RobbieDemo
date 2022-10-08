using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManger : MonoBehaviour
{
    static UIManger instance;
    public TextMeshProUGUI orbsGUI;
    public TextMeshProUGUI deathGUI;
    public TextMeshProUGUI timeGUI;
    public TextMeshProUGUI gameOverGUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public static void countOrb(int amount)
    {
        instance.orbsGUI.text = amount.ToString();
    }

    public static void countDeath(int amount)
    {
        instance.deathGUI.text = amount.ToString();
    }

    public static void countTime(float time){
        instance.timeGUI.text = time.ToString("0#:##");
    }

    public static void gameOver(){
        instance.gameOverGUI.enabled = true;
    }
}
