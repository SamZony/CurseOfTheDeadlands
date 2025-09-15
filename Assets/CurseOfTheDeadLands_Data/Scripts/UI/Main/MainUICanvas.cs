using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GameUI
{
    /// <summary>
    /// Tree-based UI navigation controller. 
    /// - startupPanel is the root node (always present). 
    /// - Each panel knows its parent and optional children.
    /// - Navigation is done by going to children or back to the parent.
    /// </summary>
    public class MainUICanvas : MonoBehaviour
    {
        private static MainUICanvas _instance;

        public static MainUICanvas Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainUICanvas();
                return _instance;
            }
        }

        [Header("Panels (root is Startup)")]
        public MainUIPanel startupPanel;
        public MainUIPanel mainMenuPanel;
        public MainUIPanel startGamePanel;
        public MainUIPanel missionSelectPanel;
        public MainUIPanel newGamePanel;
        public MainUIPanel optionsPanel;

        [Header("Gameplay Panels")]
        public GameObject missionFailPanel;
        public GameObject missionPassPanel;
        public GameHUDPanel gameHUDPanel;
        [Space]
        public GameObject pausePanel;


        private MainUIPanel currentPanel;

        [HideInInspector] public static bool isMainMenu;

        public static Action<bool> toggleIsMainMenu;
        public static Action onMissionFail;
        public static Action onMissionPass;

        #region InputSystemActions
        private InputSystem_Actions actions;
        private InputAction cancel;
        #endregion


        private void Awake()
        {
            // Singleton
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // Input
            actions = new InputSystem_Actions();
            cancel = actions.UI.Cancel;

            // Build tree relationships
            BuildTree();
        }

        private void OnEnable()
        {
            toggleIsMainMenu += (toggle) =>
            {
                isMainMenu = toggle;
                PlayerPrefs.SetInt("isMainMenu", toggle ? 1 : 0);
            };

            actions.Enable();
            cancel.performed += TryGoBack;

            onMissionFail += ShowMissionFailPanel;
            onMissionPass += ShowMissionPassPanel;
        }

        private void ShowMissionPassPanel()
        {
            Time.timeScale = 0;
            missionPassPanel.SetActive(true);
        }

        private void ShowMissionFailPanel()
        {
            Time.timeScale = 0;
            missionFailPanel.SetActive(true);
        }

        private void Start()
        {
            isMainMenu = PlayerPrefs.GetInt("isMainMenu", 1) == 1;

            Button[] buttons = GetComponentsInChildren<Button>(true); // true = include inactive

            foreach (Button btn in buttons)
            {
                btn.onClick.AddListener(() => SoundManager.Instance.PlayBTNClicked());
            }

            // Initialize UI
            HideAllPanels();
            currentPanel = startupPanel;
            currentPanel.Show();
        }

        private void OnDisable()
        {
            toggleIsMainMenu -= (toggle) => isMainMenu = toggle;
            actions.Disable();

            onMissionFail -= ShowMissionFailPanel;
            onMissionPass -= ShowMissionPassPanel;
        }

        #region Navigation

        private void TryGoBack(InputAction.CallbackContext context) => TryGoBack();

        public void TryGoBack()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (currentPanel.parent != null)
                {
                    GoToPanel(currentPanel.parent);
                }
                else
                {
                    Debug.Log("Reached root, quitting app...");
                    Application.Quit();
                }
            }
            else
            {
                if (SceneManager.GetActiveScene().name != "LoadingScreen")
                    GameManager.Instance.TogglePause();
            }
        }

        public void GoToPanel(MainUIPanel target)
        {
            Debug.Log("[GoToPanel()] Went into the method");
            if (target == null)
            {
                Debug.Log("[GoToPanel()] Target is null, returning...");
                return;
            }


            currentPanel.Hide();
            target.Show();
            currentPanel = target;
        }

        #endregion

        #region Helpers

        private void HideAllPanels()
        {
            startupPanel.Hide();
            mainMenuPanel.Hide();
            startGamePanel.Hide();
            missionSelectPanel.Hide();
            newGamePanel.Hide();
            optionsPanel.Hide();
        }

        /// <summary>
        /// Manually wire up the panel relationships here.
        /// Could be made editor-driven later.
        /// </summary>
        /// 
        public void DisableEveryPanel()
        {
            startupPanel.panelObject.SetActive(false);
            mainMenuPanel.panelObject.SetActive(false);
            startGamePanel.panelObject.SetActive(false);
            missionSelectPanel.panelObject.SetActive(false);
            newGamePanel.panelObject.SetActive(false);
            optionsPanel.panelObject.SetActive(false);
            missionFailPanel.SetActive(false);
            missionPassPanel.SetActive(false);
            gameHUDPanel.gameObject.SetActive(false);
        }
        private void BuildTree()
        {
            // Startup > Main Menu
            startupPanel.parent = null;
            startupPanel.children = new[] { mainMenuPanel };

            // Main Menu > children
            mainMenuPanel.parent = startupPanel;
            mainMenuPanel.children = new[] { startGamePanel, newGamePanel, missionSelectPanel, optionsPanel };

            // Start Game > back to Main Menu
            startGamePanel.parent = mainMenuPanel;

            // Mission Select > back to Main Menu
            missionSelectPanel.parent = mainMenuPanel;

            // New Game > back to Main Menu
            newGamePanel.parent = mainMenuPanel;

            // Options > back to Main Menu
            optionsPanel.parent = mainMenuPanel;
        }

        #endregion
    }

    [Serializable]
    public class MainUIPanel
    {
        public GameObject panelObject;

        [SerializeField] public MainUIPanel parent;

        [NonSerialized]
        public MainUIPanel[] children;

        public void Show() => panelObject?.SetActive(true);
        public void Hide() => panelObject?.SetActive(false);
    }
}
