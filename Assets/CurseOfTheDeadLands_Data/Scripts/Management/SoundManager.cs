using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource sourceBGM;
    public AudioSource sourceLooped;
    public AudioSource sourceOneShot;

    [Space]
    public AudioClips audioClips;


    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Start BGM
        sourceBGM.clip = audioClips.bgm;
        sourceBGM.Play();
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        sourceOneShot.PlayOneShot(audioClip);
    }

    public void PlayMissionBGM()
    {
        sourceBGM.clip = audioClips.bgm;
        sourceBGM.Play();
    }

    public void PlayBTNClicked()
    {
        sourceOneShot.PlayOneShot(audioClips.buttonClick);
    }

    public void PlayGoBack()
    {
        sourceOneShot.PlayOneShot(audioClips.goBack);
    }

    public void PlayStartGame()
    {
        sourceOneShot.PlayOneShot(audioClips.startGame);
    }

    public void PlayPauseGame()
    {
        sourceOneShot.PlayOneShot(audioClips.pauseGame);
    }
    public void PlayZombieChase()
    {
        sourceOneShot.PlayOneShot(audioClips.zombieChase);
        sourceBGM.clip = audioClips.fightBgm;
    }

    public void PlayZombieDie()
    {
        sourceOneShot.PlayOneShot(audioClips.zombieDie);
    }

    public void PlayPlayerHurt()
    {
        sourceOneShot.PlayOneShot(audioClips.playerHurt);
    }

    public void PlayMissionFailed()
    {
        sourceOneShot.PlayOneShot(audioClips.MissionFailed);
    }

    public void PlayMissionPassed()
    {
        sourceOneShot.PlayOneShot(audioClips.MissionPassed);
    }

    [Serializable]
    public struct AudioClips
    {
        public AudioClip bgm;

        [Header("UI")]
        public AudioClip buttonClick;
        public AudioClip goBack;
        public AudioClip startGame;
        public AudioClip pauseGame;

        [Header("Gameplay")]
        public AudioClip missionBgm;
        public AudioClip fightBgm;
        public AudioClip zombieChase;
        public AudioClip zombieDie;
        public AudioClip playerHurt;
        public AudioClip MissionFailed;
        public AudioClip MissionPassed;

    }
}
