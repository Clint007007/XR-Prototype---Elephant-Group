using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static UnityEvent OnStartGesture = new UnityEvent();
    public static UnityEvent OnGesture1     = new UnityEvent();
    public static UnityEvent OnGesture2     = new UnityEvent();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnStartGesture.Invoke();
        if (Input.GetKeyDown(KeyCode.Q))     OnGesture1.Invoke();
        if (Input.GetKeyDown(KeyCode.W))     OnGesture2.Invoke();
    }

    public static void TriggerStart()    => OnStartGesture.Invoke();
    public static void TriggerGesture1() => OnGesture1.Invoke();
    public static void TriggerGesture2() => OnGesture2.Invoke();
}