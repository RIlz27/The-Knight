using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] elektrik;
    [SerializeField] private AudioClip fireballsSound;

    [Header("Melee Attack")]
    [SerializeField] private float meleeAttackRadius = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int meleeDamage = 10;

    [Header("SFX")]
    [SerializeField] private AudioClip melee;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            MaleeAttack();
        }

        if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballsSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        elektrik[FindElektrik()].transform.position = firePoint.position;
        elektrik[FindElektrik()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void MaleeAttack()
    {
        anim.SetTrigger("melee");
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(melee);

        // Detect enemies within melee attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, meleeAttackRadius, enemyLayer);

        // Apply damage to detected enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(meleeDamage);
            }
        }
    }

    private int FindElektrik()
    {
        for (int i = 0; i < elektrik.Length; i++)
        {
            if (!elektrik[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, meleeAttackRadius);
    }
}
