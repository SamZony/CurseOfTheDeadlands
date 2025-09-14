using System;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] float minDisplayTime = 2f; // Minimum time to display the loading screen
    private string sceneName;
    private void Start()
    {
        string sceneName = PlayerPrefs.GetString("SelectedMission", "mission1");
        this.sceneName = sceneName;

        Invoke(nameof (LoadScene), minDisplayTime);

    }

    private void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }
}
