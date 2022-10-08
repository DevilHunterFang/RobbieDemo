using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager;

    [Header("音源")]
    public AudioClip[] footStepClips;
    public AudioClip[] crouchingStepClips;
    public AudioClip musicClip;
    public AudioClip ambienceClip;
    public AudioClip jumpVoiceClip;
    public AudioClip jumpClip;
    public AudioClip deathVoiceClip;
    public AudioClip deathSFXClip;
    public AudioClip collectVoiceClip;
    public AudioClip collectSFXClip;
    public AudioClip doorOpenSFXClip;
    public AudioClip winSFXClip;
    public AudioClip startSFXClip;

    public AudioSource ambienceSource;
    public AudioSource musicSource;
    public AudioSource voiceSource;
    public AudioSource sfxSource;

    public AudioMixerGroup ambientGroup, musicGroup, sfxGroup, voiceGroup;

    void Awake()
    {
        if (audioManager != null)
        {
            Destroy(gameObject);
            return;
        }
        audioManager = this;
        ambienceSource = this.gameObject.AddComponent<AudioSource>();
        musicSource = this.gameObject.AddComponent<AudioSource>();
        voiceSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        ambienceSource.outputAudioMixerGroup = ambientGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        sfxSource.outputAudioMixerGroup = sfxGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ambienceSource.clip = ambienceClip;
        ambienceSource.loop = true;
        ambienceSource.Play();

        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public static void PlaySFX(int id)
    {
        switch (id)
        {
            case 0:
                audioManager.sfxSource.clip = audioManager.footStepClips[Random.Range(0, audioManager.footStepClips.Length)];
                audioManager.sfxSource.Play();
                break;
            case 1:
                audioManager.sfxSource.clip = audioManager.crouchingStepClips[Random.Range(0, audioManager.crouchingStepClips.Length)];
                audioManager.sfxSource.Play();
                break;
            case 2:
                audioManager.sfxSource.clip = audioManager.jumpClip;
                audioManager.sfxSource.Play();
                audioManager.voiceSource.clip = audioManager.jumpVoiceClip;
                audioManager.voiceSource.Play();
                break;
            case 3:
                audioManager.sfxSource.clip = audioManager.deathSFXClip;
                audioManager.sfxSource.Play();
                audioManager.voiceSource.clip = audioManager.deathVoiceClip;
                audioManager.voiceSource.Play();
                break;
            case 4:
                audioManager.sfxSource.clip = audioManager.collectSFXClip;
                audioManager.sfxSource.Play();
                audioManager.voiceSource.clip = audioManager.collectVoiceClip;
                audioManager.voiceSource.Play();
                break;
            case 5:
                audioManager.sfxSource.clip = audioManager.doorOpenSFXClip;
                audioManager.sfxSource.PlayDelayed(1f);
                break;
            case 6:
                audioManager.ambienceSource.Stop();
                audioManager.musicSource.loop = false;
                audioManager.musicSource.clip = audioManager.winSFXClip;
                audioManager.musicSource.Play();
                break;
            case 7:
                audioManager.sfxSource.clip = audioManager.startSFXClip;
                audioManager.sfxSource.Play();
                break;
        }
    }
}
