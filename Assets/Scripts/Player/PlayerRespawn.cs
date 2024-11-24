using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
   [SerializeField] private AudioClip checkpointSound;
   private Transform currentCheckpoint; //we'll store our last checkpoint here
   private Health playerHealth;


   private void Awake()
   {
    playerHealth = GetComponent<Health>();
   }

   public void Respwan()
   {
    transform.position = currentCheckpoint.position; //Move Player to checkpoint position
    playerHealth.Respwan();//restore player health and reset animation

    // move camera checkpoint room (for this to work the checkpoint object has a placed
    // as a child of the room object )
    Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
   }

   //activate Checkpoint 
   private void OnTriggerEnter2D(Collider2D collision)
   {
    if (collision.transform.tag == "Checkpoint")
    {
        currentCheckpoint = collision.transform; //store the checkpoint that we activated as the current one
        SoundManager.instance.PlaySound(checkpointSound);
        collision.GetComponent<Collider2D>().enabled = false; //deactive checkpoint collider
        collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger checkpoint animation   
    }
   }
}
