using Unity.VisualScripting;
using UnityEngine;

public class enemy1logic : MonoBehaviour
{
    public bool facingLeft = true;
    public float moveSpeed = 3f;
    public Transform checkpoint;
    public float distance = 1f;
    public LayerMask layerMask;
    public Animator animator;
    public Transform player;
    public float visionRange = 10f;
    public float attackRange = 2f;
    public bool inRange = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= visionRange)
        {
            inRange = true;
            if (player.position .x < transform.position.x && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                transform.position = new Vector3(transform.position.x - 4f, transform.position.y, 0f);
                facingLeft = true;
            }
            else if (player.position.x > transform.position.x && facingLeft == true)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                transform.position = new Vector3(transform.position.x + 4f, transform.position.y, 0f);
                facingLeft = false;
            }   
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                // Attack the player
                Debug.Log("Attack the player");
                animator.SetBool("attack1", true);
                
            }
            else
            {
                animator.SetBool("attack1", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            inRange = false;
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(checkpoint.position, Vector2.down, distance, layerMask);

            if (hit == false && facingLeft == true)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                transform.position = new Vector3(transform.position.x + 4f, transform.position.y, 0f);
                facingLeft = false;
            }
            else if (hit == false && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                transform.position = new Vector3(transform.position.x - 4f, transform.position.y, 0f);
                facingLeft = true;
            }
        }
        

       

    }

    private void OnDrawGizmos()
    {
        if (checkpoint == null){ 
            return;}
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkpoint.position, Vector2.left * distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    
}
