using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Panels
{
    /// <summary>
    /// Panel for selecting missions to play.   
    /// </summary>
    public class MissionSelectPanel : MonoBehaviour
    {
        public List<MissionButton> missionButtons;

        public string loadingSceneName = "LoadingScreen";
        private void OnEnable()
        {
            foreach (var missionButton in missionButtons)
            {
                missionButton.missionButton.onClick.AddListener(() => SelectMission(missionButton.missionSceneName));
            }
        }
        private void OnDisable()
        {
            foreach (var missionButton in missionButtons)
            {
                missionButton.missionButton.onClick.RemoveAllListeners();
            }
        }
        private void SelectMission(string sceneName)
        {
            // Load the selected mission scene
            PlayerPrefs.SetString("SelectedMission", sceneName);
        }

        public void StartMission()
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(loadingSceneName);
            // Loading Scene has the script which on start looks for the SelectedMission PlayerPref and loads that scene.
            MainUICanvas.isMainMenu = false;
            MainUICanvas.Instance.DisableEveryPanel();
        }
    }
    [Serializable]
    public struct MissionButton
    {
        public Button missionButton;
        public string missionSceneName;
    }
}
