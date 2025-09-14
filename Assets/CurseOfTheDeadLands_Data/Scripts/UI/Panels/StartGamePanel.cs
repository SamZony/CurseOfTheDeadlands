using GameUI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace GameIU.Panels
{
    /// <summary>
    /// Represents the panel displayed at the start of the game, providing options to start a new game,  select a
    /// mission, or navigate back to the previous screen.
    /// </summary>
    /// <remarks>This panel includes three buttons: "New Game", "Mission Select", and "Back". Each button
    /// triggers  a specific action when clicked. The panel is typically part of the main user interface and interacts 
    /// with the <see cref="MainUICanvas"/> to navigate between different panels.</remarks>
    public class StartGamePanel : MonoBehaviour
    {
        public Button newGameButton;
        public Button MissionSelectButton;
        public Button backButton;

        [Header("New Game Panel")]
        public Button proceedButton;

        [Space]
        public string sceneName;

        private void OnEnable()
        {
            newGameButton?.onClick.AddListener(OnNewGameClicked);
            MissionSelectButton?.onClick.AddListener(OnMissionSelectClicked);
            backButton?.onClick.AddListener(OnBackClicked);
            proceedButton.onClick.AddListener(OnProceedClicked);
        }

        private void OnDisable()
        {
            newGameButton?.onClick.RemoveAllListeners();
            MissionSelectButton?.onClick.RemoveAllListeners();
            backButton?.onClick.RemoveAllListeners();
            proceedButton.onClick.RemoveAllListeners();
        }

        public void OnProceedClicked()
        {
            Debug.Log("Reached proceed method");
            PlayerPrefs.SetString("SelectedMission", "mission1");
            try
            {
                SceneManager.LoadScene(sceneName);
                MainUICanvas.Instance.DisableEveryPanel();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void OnBackClicked()
        {
            MainUICanvas.Instance.TryGoBack();
        }

        private void OnMissionSelectClicked()
        {
            MainUICanvas.Instance.GoToPanel(MainUICanvas.Instance.missionSelectPanel);
        }

        private void OnNewGameClicked()
        {
            MainUICanvas.Instance.GoToPanel(MainUICanvas.Instance.newGamePanel);
        }
    }
}
