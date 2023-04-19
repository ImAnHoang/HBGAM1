using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    // onTriggerEnter2d
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           enemy.SetTarget(other.GetComponent<Character>());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SetTarget(null);
        }
    }
}
