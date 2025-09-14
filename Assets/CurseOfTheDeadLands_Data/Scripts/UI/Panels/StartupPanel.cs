using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Panels
{
    public class StartupPanel : MonoBehaviour
    {
        public Button enterGameButton;
        public Button exitGameButton;

        private void OnEnable()
        {
            enterGameButton.onClick.AddListener(EnterTheGame);
            exitGameButton?.onClick.AddListener(ExitTheGame);
        }
        private void OnDisable()
        {
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
