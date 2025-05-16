using UnityEngine;
using UnityEngine.InputSystem;

public class playerLogic : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private float movement;
    public float movSpeed = 5f;
    private bool facingRight = true;
    public Animator animator;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
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

}
