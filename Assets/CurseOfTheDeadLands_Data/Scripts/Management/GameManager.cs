using GameUI;
using Invector.vShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string checkpointPlayerPref;

    [Header("References")]
    [Tooltip("The objects which have to be disabled when Main UI is in action or they will interfere.")]
    public List<GameObject> objectsAgainstMainUI;

    [Header("Mission Parameters")]
    public int enemiesDead = 0;
    public int totalEnemies = 1;

    [Space]
    public int currentObjective;
    public int maxObjectives;

    [HideInInspector]
    public bool isMissionPassed;
    public bool isGameplayPaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        SoundManager.Instance.sourceBGM.clip = SoundManager.Instance.audioClips.missionBgm;
        SoundManager.Instance.sourceBGM.Play();
    }
    public void ToggleHUD(bool toggle)
    {
            MainUICanvas.Instance.gameHUDPanel.gameObject.SetActive(toggle);
    }


    public void IncrementEnemiesDead()
    {
        enemiesDead++;
        if (enemiesDead >= totalEnemies && currentObjective >= maxObjectives)
        {
            if (!isMissionPassed)
                PassMission();
        }
    }

    public void IncrementObjectives()
    {
        currentObjective++;
        if (enemiesDead >= totalEnemies && currentObjective >= maxObjectives)
        {
            if (!isMissionPassed)
                PassMission();
        }
    }

    public void FailMission()
    {
        for (int i = 0; i < objectsAgainstMainUI.Count; i++)
        {
            objectsAgainstMainUI[i].SetActive(false);
        }
        ToggleHUD(false);

        MainUICanvas.onMissionFail?.Invoke();
    }
    public void PassMission()
    {
        for (int i = 0; i < objectsAgainstMainUI.Count; i++)
        {
            objectsAgainstMainUI[i].SetActive(false);
        }
        ToggleHUD(false);

        MainUICanvas.onMissionPass?.Invoke();
    }

    public void TogglePause()
    {
        if (isGameplayPaused)
        {
            Time.timeScale = 1;
            for (int i = 0; i < objectsAgainstMainUI.Count; i++)
            {
                objectsAgainstMainUI[i].SetActive(true);
            }
            ToggleHUD(true);

            isGameplayPaused = false;
            MainUICanvas.Instance.pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            for (int i = 0; i < objectsAgainstMainUI.Count; i++)
            {
                objectsAgainstMainUI[i].SetActive(false);
            }
            ToggleHUD(false);

            isGameplayPaused = true;
            MainUICanvas.Instance.pausePanel.SetActive(true);
        }
    }

    public Vector3 LoadCheckpointPosition()
    {
        string posString = PlayerPrefs.GetString(checkpointPlayerPref, "0,0,0");
        string[] values = posString.Split(',');
        Vector3 savedPos = new Vector3(
            float.Parse(values[0]),
            float.Parse(values[1]),
            float.Parse(values[2])
        );
        return savedPos;
    }
}
