using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    [Header("SFX")]
    [SerializeField] private AudioClip spikeSound;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(spikeSound);
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}