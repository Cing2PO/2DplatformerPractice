using UnityEngine;

public class enemy1logic : MonoBehaviour
{
    public bool facingLeft = true;
    public float moveSpeed = 3f;
    public Transform checkpoint;
    public float distance = 1f;
    public LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(checkpoint.position, Vector2.left, distance, layerMask);

        if (hit == false && facingLeft == true)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingLeft = false;
        }
        else if (hit == true && facingLeft == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingLeft = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (checkpoint == null){ 
            return;}
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkpoint.position, Vector2.left * distance);

    }

    
}
