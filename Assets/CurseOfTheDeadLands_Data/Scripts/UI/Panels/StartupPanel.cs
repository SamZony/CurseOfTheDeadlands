using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameUI.Panels
{
    public class StartupPanel : MonoBehaviour
    {
        public Button enterGameButton;
        public Button exitGameButton;

        private void OnEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            SceneManager.sceneLoaded += OnSceneLoaded;

            enterGameButton.onClick.AddListener(EnterTheGame);
            exitGameButton?.onClick.AddListener(ExitTheGame);
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"Scene Loaded: {scene.name}, Mode: {mode}");

            if (scene.buildIndex == 0)
            {
                MainUICanvas.isMainMenu = true;
                MainUICanvas.Instance.GoToPanel(MainUICanvas.Instance.startupPanel);
            }
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            enterGameButton.onClick.RemoveAllListeners();
            exitGameButton?.onClick.RemoveAllListeners();
        }

        private void ExitTheGame()
        {
            Application.Quit();
        }

        public void EnterTheGame()
        {
            MainUICanvas.Instance.GoToPanel(MainUICanvas.Instance.mainMenuPanel);
        }
    }
}
