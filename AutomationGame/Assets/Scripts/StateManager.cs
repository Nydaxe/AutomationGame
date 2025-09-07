using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    public State state;

    public enum State
    {
        Idle,
        PlacingTube,
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        state = State.Idle;
    }

    public void SetState(State newState)
    {
        state = newState;
    }
}
