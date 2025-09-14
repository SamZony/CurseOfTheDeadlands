using System;
using UnityEngine;
using UnityEngine.UI;


namespace GameUI.Panels
{
    /// <summary>
    /// Panel for pausing the game and accessing pause menu options.
    /// </summary>
    public class PausePanel : MonoBehaviour
    {
        public Button resumeButton;
        public Button restartMission;
        public Button mainMenuButton;

        private void OnEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            resumeButton.onClick.AddListener(ResumeGame);
            restartMission.onClick.AddListener(RestartMission);
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
            Time.timeScale = 0f; // Pause the game
        }

        private void RestartMission()
        {
            // Reload the current scene
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            MainUICanvas.isMainMenu = false;
        }

        private void ReturnToMainMenu()
        {
            // Load the main menu scene
            Time.timeScale = 1f; // Resume the game before loading main menu    
            MainUICanvas.isMainMenu = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        private void ResumeGame()
        {
            Time.timeScale = 1f;
            GameManager.Instance.TogglePause();
        }

        private void OnDisable()
        {
            Time.timeScale = 1f; // Resume the game
            resumeButton.onClick.RemoveAllListeners();
            restartMission.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
