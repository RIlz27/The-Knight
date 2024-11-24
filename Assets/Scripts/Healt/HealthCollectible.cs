using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    [Header("SFX")]
    [SerializeField] private AudioClip pickupSound;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boxCollider.enabled = false;
        
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            anim.SetTrigger("take");
            // gameObject.SetActive(false);
        }
    }
}
