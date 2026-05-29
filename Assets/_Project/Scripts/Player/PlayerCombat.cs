using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerCombat : MonoBehaviour
{
    [Header("Melee")]
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public int attackDamage = 25;
    public float attackCooldown = 0.5f;
    public LayerMask enemyLayer;

    [Header("Ranged (optional)")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float cooldownTimer = 0f;
    private Animator animator;

    void Start() => animator = GetComponent<Animator>();

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0f)
        {
            MeleeAttack();
            cooldownTimer = attackCooldown;
        }

        if (bulletPrefab != null && Input.GetButtonDown("Fire2") && cooldownTimer <= 0f)
        {
            RangedAttack();
            cooldownTimer = attackCooldown;
        }
    }

    void MeleeAttack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var hit in hits)
            hit.GetComponent<ZombieHealth>()?.TakeDamage(attackDamage);
    }

    void RangedAttack()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}