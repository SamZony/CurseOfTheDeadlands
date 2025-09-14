using GameUI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string checkpointPlayerPref;

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
    public void ShowHUD()
    {
            MainUICanvas.Instance.gameHUDPanel.gameObject.SetActive(true);
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
        MainUICanvas.onMissionFail?.Invoke();
    }
    public void PassMission()
    {
        MainUICanvas.onMissionPass?.Invoke();
    }

    public void TogglePause()
    {
        if (isGameplayPaused)
        {
            Time.timeScale = 1;
            isGameplayPaused = false;
            MainUICanvas.Instance.pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;

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
