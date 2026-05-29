using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ScrollingCity : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float scrollSpeed = 3f;
    public bool isScrolling = true;

    [Header("City Bounds")]
    public float cityEndX = 500f; // how long the street is

    private Camera mainCamera;
    private float startX;

    public static ScrollingCity Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        mainCamera = Camera.main;
        startX = mainCamera.transform.position.x;

        // Hook into game over to stop scrolling
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameOver += StopScrolling;
    }

    void Update()
    {
        if (!isScrolling) return;

        // Move camera forward
        mainCamera.transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

        // Stop at city end
        if (mainCamera.transform.position.x >= startX + cityEndX)
            StopScrolling();
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }

    public void ResumeScrolling()
    {
        isScrolling = true;
    }

    public float GetScrollProgress()
    {
        return (mainCamera.transform.position.x - startX) / cityEndX;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameOver -= StopScrolling;
    }
}