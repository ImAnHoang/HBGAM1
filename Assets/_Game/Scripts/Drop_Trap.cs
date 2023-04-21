using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Trap : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Healthbar healthbar;

    private bool isDroped = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isDroped = true;
        }
        if (collision.gameObject.tag == "Player" && isDroped == false)
        {
            player.hp -= 50;
            healthbar.SetNewHp(player.hp);
        }



    }
}
