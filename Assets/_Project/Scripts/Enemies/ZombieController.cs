using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ZombieController : MonoBehaviour
{
    public float moveSpeed = 1.8f;
    public float detectionRange = 6f;
    public float attackRange = 0.6f;
    public int attackDamage = 10;
    public float attackCooldown = 1.2f;

    private Transform player;
    private float attackTimer = 0f;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        attackTimer -= Time.deltaTime;

        if (dist <= attackRange)
        {
            Attack();
        }
        else if (dist <= detectionRange)
        {
            Chase();
        }
        else
        {
            Idle();
        }
    }

    void Chase()
    {
        animator.SetBool("IsWalking", true);
        Vector2 dir = (player.position - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        // Flip to face player
        transform.localScale = new Vector3(
        dir.x < 0 ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x),
            transform.localScale.y, transform.localScale.z);
    }

    void Attack()
    {
        animator.SetBool("IsWalking", false);
        if (attackTimer <= 0f)
        {
            animator.SetTrigger("Attack");
            player.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
            attackTimer = attackCooldown;
        }
    }

    void Idle()
    {
        animator.SetBool("IsWalking", false);
    }
}