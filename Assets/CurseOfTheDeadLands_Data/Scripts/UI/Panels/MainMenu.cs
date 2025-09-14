using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Panels
{
    /// <summary>
    /// Represents the main menu of the application, providing buttons for starting the game,  opening the options menu,
    /// and exiting the application.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [Header("Main Buttons")]
        public Button startGameBTN;
        public Button optionsBTN;
        public Button exitBTN;


        public void OnEnable()
        {
            startGameBTN.onClick.AddListener(StartGame);
            optionsBTN.onClick.AddListener(OpenOptions);
            exitBTN.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void OpenOptions()
        {
            MainUICanvas.Instance.GoToPanel(MainUICanvas.Instance.optionsPanel);
        }

        private void StartGame()
        {
            MainUICanvas.Instance.GoToPanel(MainUICanvas.Instance.startGamePanel);
        }

        private void OnDisable()
        {
            startGameBTN.onClick?.RemoveAllListeners();
            optionsBTN.onClick?.RemoveAllListeners();
            exitBTN.onClick?.RemoveAllListeners();
        }
    }
}
