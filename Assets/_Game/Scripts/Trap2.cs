using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            rb.gravityScale = 2;
        }
        
      
        
    }
}
