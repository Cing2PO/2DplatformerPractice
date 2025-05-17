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
    public Transform attackPoint;
    public LayerMask attackLayer;
    public int HP = 100;
    public int attackDamage = 10;
    public int attackCount = 0;
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
                animator.SetBool("walk", false);
                if (attackCount < 2)
                {
                    
                    animator.SetBool("attack1", true);
                    animator.SetBool("attack2", false);
                }
                else if (attackCount == 2)
                {
                    animator.SetBool("attack2", true);
                    animator.SetBool("attack1", false);
                    
                }
               
                
                
            }
            else
            {
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", false);
                animator.SetBool("walk", true);
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

    public void Attack1()
    {
        Collider2D atkCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackLayer);

        if (atkCollider)
        {
            if (atkCollider.gameObject.GetComponent<playerLogic>() != null)
            {
                atkCollider.gameObject.GetComponent<playerLogic>().takingDamage(attackDamage);
                attackCount += 1;
            }
        }
    }

    public void Attack2()
    {
        Collider2D atkCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackLayer);
        

        if (atkCollider)
        {
            if (atkCollider.gameObject.GetComponent<playerLogic>() != null)
            {
                atkCollider.gameObject.GetComponent<playerLogic>().takingDamage(attackDamage*2);
                attackCount = 0;
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
}
