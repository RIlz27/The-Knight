// using UnityEngine;

// public class SpikeHeadcopy : MonoBehaviour
// {
//     [SerializeField] private float range;
//     [SerializeField] private float speed;
//     [SerializeField] private LayerMask playerLayer;
//     private Vector3[] directions = new Vector3[4];
//     private Vector3 destination;
//     private float attackTimer = Mathf.Infinity;

//     private void Start()
//     {
//         Stop();
//     }
//     private void CalculateDirections()
//     {
//         directions[0] = transform.position - transform.right * range;//Left direction
//         directions[1] = transform.position + transform.right * range;//Right direction
//         directions[2] = transform.position + transform.up * range;   //Up direction
//         directions[3] = transform.position - transform.up * range;   //Down direction
//     }
//     private void Update()
//     {
//         attackTimer += Time.deltaTime;
//         if (attackTimer >= 1) CheckForPlayer();
//         transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speed);
//     }
//     private void CheckForPlayer()
//     {
//         print("checking");
//         CalculateDirections();
//         for (int i = 0; i < directions.Length; i++)
//         {
//             RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], Mathf.Infinity, playerLayer);

//             if (hit.collider != null)
//             {
//                 print(hit.collider.name);
//                 destination = directions[i];
//                 attackTimer = 0;
//             }
//         }
//     }
//     private void Stop()
//     {
//         destination = transform.position;
//         attackTimer = 0;
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         Stop();
//         if (collision.gameObject.tag == "Player")
//             collision.gameObject.GetComponent<Health>().TakeDamage(1);
//     }
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.tag == "Player")
//             destination = collision.transform.position;
//     }
//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         for (int i = 0; i < directions.Length; i++)
//             Gizmos.DrawLine(transform.position, directions[i]);
//     }
// }







// using UnityEngine;

// public class EnemyPatrol : MonoBehaviour
// {
//     [Header ("Patrol Points")]
//     [SerializeField] private Transform leftEdge;
//     [SerializeField] private Transform rightEdge;

//     [Header("Enemy")]
//     [SerializeField] private Transform enemy;

//     [Header("Movement parameters")]
//     [SerializeField] private float speed;
//     private Vector3 initScale;
//     private bool movingLeft;

//     [Header("Idle Behaviour")]
//     [SerializeField] private float idleDuration;
//     private float idleTimer;

//     [Header("Enemy Animator")]
//     [SerializeField] private Animator anim;

//     private void Awake()
//     {
//         initScale = enemy.localScale;
//     }
//     private void OnDisable()
//     {
//         anim.SetBool("moving", false);
//     }

//     private void Update()
//     {
//         if (movingLeft)
//         {
//             if (enemy.position.x >= leftEdge.position.x)
//                 MoveInDirection(-1);
//             else
//                 DirectionChange();
//         }
//         else
//         {
//             if (enemy.position.x <= rightEdge.position.x)
//                 MoveInDirection(1);
//             else
//                 DirectionChange();
//         }
//     }

//     private void DirectionChange()
//     {
//         anim.SetBool("moving", false);
//         idleTimer += Time.deltaTime;

//         if(idleTimer > idleDuration)
//             movingLeft = !movingLeft;
//     }

//     private void MoveInDirection(int _direction)
//     {
//         idleTimer = 0;
//         anim.SetBool("moving", true);

//         //Make enemy face direction
//         enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
//             initScale.y, initScale.z);

//         //Move in that direction
//         enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
//             enemy.position.y, enemy.position.z);
//     }
// }