using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;

public class FriendNPC : MonoBehaviour
{
    [Header("Friend Info")]
    public string friendName = "Alex";
    public bool isRescued = false;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public GameObject rescueEffect; // optional particle effect

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isRescued)
            Rescue();
    }

    void Rescue()
    {
        isRescued = true;

        // Notify game manager and HUD
        GameManager.Instance.RescueFriend();
        HUDManager.Instance?.ShowNotification($"{friendName} has been rescued!");

        // Play rescue effect if assigned
        if (rescueEffect)
            Instantiate(rescueEffect, transform.position, Quaternion.identity);

        // Play animation if available
        if (animator)
            animator.SetTrigger("Rescued");

        Debug.Log($"{friendName} has been rescued!");

        // Disable after short delay to allow animation to play
        Invoke(nameof(Disable), 0.5f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}