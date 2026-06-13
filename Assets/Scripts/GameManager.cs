using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { WaitingToStart, ElephantWalking, Idle, Resolving }
    public GameState CurrentState { get; private set; } = GameState.WaitingToStart;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        InputManager.OnStartGesture.AddListener(HandleStart);
        InputManager.OnGesture1.AddListener(HandleGesture1);
        InputManager.OnGesture2.AddListener(HandleGesture2);
    }

    void OnDisable()
    {
        InputManager.OnStartGesture.RemoveListener(HandleStart);
        InputManager.OnGesture1.RemoveListener(HandleGesture1);
        InputManager.OnGesture2.RemoveListener(HandleGesture2);
    }

    void HandleStart()
    {
        if (CurrentState != GameState.WaitingToStart) return;
        CurrentState = GameState.ElephantWalking;
        ElephantController.Instance.StartWalking();
    }

    void HandleGesture1()
    {
        if (CurrentState != GameState.Idle) return;
        CurrentState = GameState.Resolving;
        ElephantController.Instance.PlayAttack();
    }

    void HandleGesture2()
    {
        if (CurrentState != GameState.Idle) return;
        CurrentState = GameState.Resolving;
        ElephantController.Instance.PlayWalkAway();
    }

    public void SetIdle() => CurrentState = GameState.Idle;

    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScreen");
    }
}