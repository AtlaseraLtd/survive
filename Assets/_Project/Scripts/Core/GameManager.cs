using System.Diagnostics;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int friendsRescued = 0;
    public int totalFriends = 4;
    public bool isGameOver = false;

    public event System.Action OnGameOver;
    public event System.Action<int> OnFriendRescued;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RescueFriend()
    {
        friendsRescued++;
        OnFriendRescued?.Invoke(friendsRescued);

        if (friendsRescued >= totalFriends)
            TriggerVictory();
    }

    void TriggerVictory()
    {
        Debug.Log("All friends rescued — You Win!");
        // TODO: load victory screen
    }

    public void TriggerGameOver(string cause = "Zombies")
    {
        if (isGameOver) return;
        isGameOver = true;

        GameOverData.causeOfDeath = cause;
        GameOverData.survivalTime = SurvivalTimer.ElapsedTime;

        OnGameOver?.Invoke();

        StartCoroutine(LoadGameOverScreen());
    }

    public static void LoadSceneWithLoading(string sceneName)
    {
        LoadingScreenData.sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSeconds(2f); // brief delay for death animation
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

}