using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    void OnEnable()
    {
        InputManager.OnStartGesture.AddListener(LoadMainScene);
    }

    void OnDisable()
    {
        InputManager.OnStartGesture.RemoveListener(LoadMainScene);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}