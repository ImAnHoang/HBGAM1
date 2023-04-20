using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hitVFX;
    public Rigidbody2D rb;

    void Start()
    {
 
        OnInit();
    }

    // Update is called once per frame
    public void OnInit()
    {
        rb.velocity = transform.right * 6f;
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnDespawn()
    {
        // Destroy(hitVFX.gameObject, 0.5f);
        Destroy(gameObject);
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {

            collision.GetComponent<Enemy>().OnHit(30f);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn(); 
        }
    }
}
