using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathPanel;

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

    public void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        isShowing = true;

        // Freeze everything
        Time.timeScale = 0f;
        AudioListener.pause = true;

        // Free the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}