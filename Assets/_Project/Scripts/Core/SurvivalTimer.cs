using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    public static float ElapsedTime { get; private set; }
    private bool running = true;

    void Start()
    {
        ElapsedTime = 0f;
        GameManager.Instance.OnGameOver += StopTimer;
    }

    void Update()
    {
        if (running)
            ElapsedTime += Time.deltaTime;
    }

    void StopTimer()
    {
        running = false;
        GameOverData.survivalTime = ElapsedTime;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameOver -= StopTimer;
    }
}