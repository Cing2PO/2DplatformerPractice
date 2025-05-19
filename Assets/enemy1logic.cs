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
    public float attackInterval = 5f;
    public float attackTimer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<gameManager>().isGameActive == false)
        {
            animator.SetBool("walk", false);
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", false);
            return;
        }
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
        else if (attackTimer <= 0f)
        {
            attackTimer = 0f;
        }
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
            if ((Vector2.Distance(transform.position, player.position) <= attackRange*2) && attackTimer <= 0f)
            {
                animator.SetBool("walk", false);
                if (attackCount < 2)
               {
                    animator.SetBool("attack1",true);
               }
                else if (attackCount >= 2)
               {
                    animator.SetBool("attack2",true);

               }
            }
            else
            {
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
        
        if (HP <= 0f)
        {
            Death();
        }
       

    }

    public void Attack1()
    {
        Collider2D atkCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackLayer);

        if (atkCollider)
        {
            attackCount++;
            if (atkCollider.gameObject.GetComponent<playerLogic>() != null)
            {
                atkCollider.gameObject.GetComponent<playerLogic>().takingDamage(attackDamage);
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
            }
        }
    }
    public void attackreset()
    {
        attackTimer = attackInterval;
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
        if (attackCount >= 2)
        {
            attackCount = 0;
        }
        else if (attackCount < 2)
        {
            attackCount++;
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

    public void takingDamage(int damagetaken)
    {
        if (HP <= 0f)
        {
            return;
        }
        else if (HP > 0f)
        {
            HP -= damagetaken;
            animator.SetTrigger("Hit");
        }
        
    }
    
    private void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 1.5f);
    }
}
