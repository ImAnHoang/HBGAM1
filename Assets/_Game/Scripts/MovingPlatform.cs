using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform startPoint, endPoint;
    [SerializeField] private float speed = 5;
    Vector3 target;

    void Start()
    {
        transform.position = startPoint.position;
        target = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, startPoint.position) < 0.1f)
        {
            target = endPoint.position;
        }else if(Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            target = startPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
