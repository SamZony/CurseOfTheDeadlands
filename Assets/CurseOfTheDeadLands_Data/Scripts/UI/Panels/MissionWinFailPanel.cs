using Invector;
using Invector.vCharacterController;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameUI.Panels
{
    /// <summary>
    /// Panel for handling mission win and fail states. 
    /// </summary>
    public class MissionWinFailPanel : MonoBehaviour
    {
        public FailPanel failPanel;
        public WinPanel winPanel;

        public bool isWinPanel;

        private void OnEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            failPanel.restartMission?.onClick.AddListener(RestartMission);
            failPanel.restartCheckpoint?.onClick.AddListener(RestartCheckpoint);
            failPanel.mainMenu?.onClick.AddListener(ToMainMenu);

            winPanel.nextMission?.onClick.AddListener(LoadNextMission);
            winPanel.restartMisson?.onClick.AddListener(RestartMission);
            winPanel.settings?.onClick.AddListener(() => MainUICanvas.Instance.GoToPanel(MainUICanvas.Instance.optionsPanel));
            winPanel.mainMenu?.onClick.AddListener(ToMainMenu);

            if (isWinPanel)
                SoundManager.Instance.PlayMissionPassed();
            else
                SoundManager.Instance.PlayMissionFailed();
        }

        private void LoadNextMission()
        {
            var nextMissionIndex = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log($"Mission name: {nextMissionIndex}");
            Scene nextMission = SceneManager.GetSceneByBuildIndex(nextMissionIndex);

            PlayerPrefs.SetString("SelectedMission", nextMission.name);
            SceneManager.LoadScene("LoadingScreen");
            MainUICanvas.Instance.DisableEveryPanel();
        }

        private void ToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        private void RestartCheckpoint()
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Time.timeScale = 1;
                player.GetComponent<vHealthController>().ResetHealth();
                player.transform.position = GameManager.Instance.LoadCheckpointPosition();

            }
            MainUICanvas.Instance.DisableEveryPanel();

        }

        private void RestartMission()
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            MainUICanvas.Instance.DisableEveryPanel();

        }

        private void OnDisable()
        {
            failPanel.restartMission?.onClick.RemoveAllListeners();
            failPanel.restartCheckpoint?.onClick.RemoveAllListeners();
            failPanel.mainMenu?.onClick.RemoveAllListeners();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        [Serializable]
        public struct FailPanel
        {
            public Button restartMission;
            public Button restartCheckpoint;
            public Button mainMenu;
        }

        [Serializable]
        public struct WinPanel
        {
            public Button nextMission;
            public Button restartMisson;
            public Button settings;
            public Button mainMenu;
        }
    }
}


