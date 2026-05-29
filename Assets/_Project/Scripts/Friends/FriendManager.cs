using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class FriendManager : MonoBehaviour
{
    public static FriendManager Instance { get; private set; }

    [Header("Friends in Scene")]
    public FriendNPC[] friends;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        // Auto-find all friends in scene if not manually assigned
        if (friends == null || friends.Length == 0)
            friends = FindObjectsByType<FriendNPC>(FindObjectsInactive.Exclude);

        // Sync total friends count with GameManager
        if (GameManager.Instance != null)
            GameManager.Instance.totalFriends = friends.Length;

        Debug.Log($"FriendManager: {friends.Length} friends found in scene.");
    }

    public FriendNPC[] GetRemainingFriends()
    {
        return System.Array.FindAll(friends, f => !f.isRescued);
    }
}