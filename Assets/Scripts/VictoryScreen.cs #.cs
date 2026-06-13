using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryPanel;

    bool isShowing = false;

    void Update()
    {
        if (!isShowing) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            SceneManager.LoadScene("StartScreen");
        }
    }

    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
        isShowing = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}