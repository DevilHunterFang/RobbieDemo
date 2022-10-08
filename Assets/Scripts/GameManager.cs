using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Fader fader;
    public int deathAmount;
    public float time;
    private List<Orb> orbs;
    private Door door;
    public static bool isGameOver;

    private void Awake()
    {
        AudioManager.PlaySFX(7);
        if (instance != null)
        {
            instance.orbs.Clear();
            Destroy(gameObject);
            return;
        }
        instance = this;
        instance.orbs = new List<Orb>();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            time += Time.deltaTime;
            UIManger.countTime(instance.time);
        }
    }

    public static void RegisterFader(Fader fader)
    {
        instance.fader = fader;
    }

    public static void RegisterOrb(Orb orb)
    {
        if (!instance.orbs.Contains(orb))
            instance.orbs.Add(orb);
        UIManger.countOrb(instance.orbs.Count);
    }

    public static void RegisterDoor(Door door)
    {
        instance.door = door;
    }

    public static void GainOrb(Orb orb)
    {
        if (instance.orbs.Contains(orb))
        {
            instance.orbs.Remove(orb);
            UIManger.countOrb(instance.orbs.Count);
        }
        if (instance.orbs.Count == 0)
            instance.door.Open();
    }

    public static void PlayerDied()
    {
        instance.fader.setTrigger();
        instance.Invoke("ReloadScene", 0.9f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        instance.deathAmount += 1;
        UIManger.countDeath(instance.deathAmount);
        instance.time = 0f;
    }

    public static void PlayerWon(){
        isGameOver = true;
        UIManger.gameOver();
    }
}
