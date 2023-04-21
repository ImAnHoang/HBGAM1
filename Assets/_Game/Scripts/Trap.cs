using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody2D rb;

    
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke(nameof(ActiveTrap), 0.2f);
        }
    }
  
    private void ActiveTrap()
    {
        rb.gravityScale = 9;
    }
}
