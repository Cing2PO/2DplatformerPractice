using System.Xml.Xsl;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class bgcontroller : MonoBehaviour
{
    public float startpos, lenght;
    public GameObject cam;
    public float speed = 0.5f;

    void Start()
    {
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * speed;
        float movement = cam.transform.position.x * (1 - speed);

        transform.position = new Vector3(startpos + movement, transform.position.y, transform.position.z);

        if (movement > startpos + lenght)
        {
            startpos += lenght;
        }
        else if (movement < startpos - lenght)
        {
            startpos -= lenght;
        }
    }
}
