using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KnightBossLogic : MonoBehaviour
{
    public bool isActive;
    public bool facingLeft = true;
    public float moveSpeed = 3f;
    public int HP;
    public int maxHP = 500;
    public int attackDamage = 20;
    public float attackRange = 2f;
    public float attackTrigger = 3f;
    public float attackInterval = 5f;
    public float attackTimer = 0f;
    public float visionRange = 10f;
    private int atktype;
    public float dashPower = 10f;
    public float dashTime = 0.2f;
    public Animator animator;
    public Transform player;
    public LayerMask attackLayer;
    public Transform attackPoint;
    public GameObject HPbar;

    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private TrailRenderer trail;

    void Start()
    {
        isActive = false;
        HP = maxHP;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= visionRange){
            HPbar.SetActive(true);
            enterPhase1();
        }
        if (HP <= 0) 
        {
            isActive = false;
            animator.SetTrigger("Death");
        }
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if (player.position.x < transform.position.x && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                facingLeft = true;
            }
            else if (player.position.x > transform.position.x && facingLeft == true)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                facingLeft = false;
            }
            if (attackTimer > 0f)
            {
                attackTimer -= Time.deltaTime;
            }
            //if (vector2.distance(transform.position, player.position) <= attacktrigger && attacktimer <= 0f)
            //{
            //    animator.settrigger("attack1");
            //}

            if(attackTimer<= 0f)
            {
                atktype = Random.Range(0, 6);
                if (atktype == 0)
                {
                    animator.SetTrigger("attack1");
                    attackTimer = attackInterval;
                }
                else if (atktype == 1)
                {
                    animator.SetTrigger("attack1");
                    attackTimer = attackInterval;
                }
                else if (atktype == 2)
                {
                    animator.SetTrigger("attack1");
                    attackTimer = attackInterval;
                }
                else if (atktype == 3)
                {
                    animator.SetTrigger("dash");
                    attackTimer = attackInterval;
                    Dash();
                }
                else if (atktype == 4)
                {
                    animator.SetTrigger("dash");
                    attackTimer = attackInterval;
                    Dash();
                }
                else if (atktype == 5)
                {
                    animator.SetTrigger("dash");
                    attackTimer = attackInterval;
                    Dash();
                }
            }
        }
    }

    public void enterPhase1()
    {
        animator.SetBool("phase1", true);
    }
    public void enterPhase1End()
    {
        isActive = true;
        animator.SetBool("walk", true);
    }

    public void enterPhase2()
    {
        animator.SetBool("phase2", true);
    }

    public void jump()
    {
        animator.SetTrigger("jump");
        transform.position = new Vector2(transform.position.x, transform.position.y + 10f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * 2f * Time.deltaTime);
        attackTimer = attackInterval;
    }
    public void plunge()
    {
        animator.SetTrigger("plunge");
        transform.position = new Vector2(transform.position.x, transform.position.y - 10f);
    }

    public void Attack1()
    {
        Collider2D atkCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange*2, attackLayer);

        attackTimer = attackInterval;
        if (atkCollider)
        {
            if (atkCollider.gameObject.GetComponent<playerLogic>() != null)
            {
                atkCollider.gameObject.GetComponent<playerLogic>().takingDamage(attackDamage);
            }
        }
    }

    private IEnumerator Dash()
    {
        float originalGravity = rigidbody.gravityScale;
        rigidbody.gravityScale = 0f;
        if (!facingLeft)
        {
            rigidbody.AddForceX(transform.localScale.x * dashPower, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody.AddForceX(-transform.localScale.x * dashPower, ForceMode2D.Impulse);
        }
        animator.SetTrigger("dash");
        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        rigidbody.gravityScale = originalGravity;

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

    public void dead()
    {
        
        FindObjectOfType<gameManager>().Winning();
        Destroy(gameObject, 0f);
    }


    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
