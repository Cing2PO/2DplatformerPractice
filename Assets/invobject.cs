using UnityEngine;

public class invobject : MonoBehaviour
{
    public float speed = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move to right slowly
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("checkpoint"))
        {
            transform.position = new Vector3(transform.position.x - 200f, transform.position.y, transform.position.z);
        }
    }
}
