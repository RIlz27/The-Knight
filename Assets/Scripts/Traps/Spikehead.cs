using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpikeHead : EnemyDamage
{   
    [Header("SpikeHead Atributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;


    private bool attacking;



    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);

        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        //check if spikehead sees player
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }


    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //Right direction
        directions[1] = -transform.right * range; //left direction
        directions[2] = transform.up * range; //down direction
        directions[3] = -transform.up * range; //up direction
    }

    private void Stop()
    {
        destination = transform.position; //set destination as current possition so it doesn't move
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}