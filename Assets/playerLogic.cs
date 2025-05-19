using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerLogic : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private float movement;
    public float movSpeed = 5f;
    private bool facingRight = true;
    public Animator animator;
    public int HP;
    public int maxHP = 100;
    public int attackDamage = 50;
    public float atkInterval = 1f;
    public float atkTimer = 0f;
    public Transform attackPoint;
    public LayerMask attackLayer;
    public float attackRange = 2f;
    
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<gameManager>().isGameActive == false)
        {
            animator.SetFloat("Run", 0f);
            animator.SetBool("Jump", false);
            return;
        }
        //update HP text
        movement = Input.GetAxis("Horizontal");
        //flip player
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("Jump", true);
            jump();
            isGrounded = false;
        }

        if(Mathf.Abs(movement) > 0f)
        {
            animator.SetFloat("Run", 1f);
        }
        else
        {
            animator.SetFloat("Run", 0f);
        }

        //attack
        if (atkTimer > 0f)
        {
            atkTimer -= Time.deltaTime;
        }
        else if (atkTimer < 0f)
        {
            atkTimer = 0f;
        }
        if(Input.GetMouseButtonDown(0) && atkTimer <= 0f)
        {
            animator.SetTrigger("Attack1");
            atkTimer = atkInterval;
        }


        if (HP <= 0f)
        {
            Death();
        }

    }

    private void FixedUpdate()
    {
        //player movement
        transform.position += new Vector3(movement , 0f, 0f) * Time.fixedDeltaTime * movSpeed;
        

    }

    void jump()
    {
        rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
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

    public void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 2f);
        FindObjectOfType<gameManager>().isGameActive = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack1()
    {
        Collider2D atkCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackLayer);
        if (atkCollider)
        {
            if (atkCollider.gameObject.GetComponent<enemy1logic>() != null)
            {
                atkCollider.gameObject.GetComponent<enemy1logic>().takingDamage(attackDamage);
            }
        }
    }
}
