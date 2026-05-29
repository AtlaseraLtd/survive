using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI friendsRescuedText;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI causeOfDeathText;

    void Start()
    {
        PopulateStats();
    }

    void PopulateStats()
    {
        // Friends rescued
        if (friendsRescuedText && GameManager.Instance != null)
            friendsRescuedText.text = $"Friends Rescued: {GameManager.Instance.friendsRescued} / {GameManager.Instance.totalFriends}";

        // Survival time (stored as a static before scene change)
        if (survivalTimeText)
            survivalTimeText.text = $"Survived: {GameOverData.survivalTime:F1}s";

        // Cause of death
        if (causeOfDeathText)
            causeOfDeathText.text = $"Killed by: {GameOverData.causeOfDeath}";
    }

    public void Retry()
    {
        GameManager.LoadSceneWithLoading("GameScene");
    }

    public void MainMenu()
    {
        GameManager.LoadSceneWithLoading("MainMenu");
    }
}