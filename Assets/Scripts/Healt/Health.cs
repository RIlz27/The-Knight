using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    [Header("Componen")]
    [SerializeField] private Behaviour[] components;
    private bool invunerable;

    [Header("death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invunerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
            // iframes
        }

        else
        {
            if (!dead)
            {

                //deactive all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                SoundManager.instance.PlaySound(deathSound);
                dead = true;

            }
        }

    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respwan()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        //active all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
    private IEnumerator Invunerability()
    {
        invunerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invunerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
