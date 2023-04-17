using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;

    private bool isDie = false;
    private string currentAnim;
    private float horizontal;

    private int coin = 0;

    private Vector3 savePoint;
    [SerializeField] private float jumpForce = 350;
    void Start()
    {
        // savePoint = transform.position;
        SavePoint();
        OnInit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDie)
        {
            return;
        }
        isGrounded = CheckGrounded();

        // -1 --> 0 --> 1
        horizontal = Input.GetAxisRaw("Horizontal");
        // vertical = Input.GetAxisRaw("Vertical");

        if(isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }


            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            if(Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
            }
            if(Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }
            


            


            
        }
        // check falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }





        if (Mathf.Abs(horizontal) > 0.1f)
        {
            // ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);

            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }


    }

    public void OnInit()
    {
        isDie = false;
        isAttack = false;

        transform.position = savePoint;

        ChangeAnim("idle");
    }
    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;

    }


    private void Attack()
    {
        
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void Throw()
    {
       
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("ilde");
    }
  

    private void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coin++;
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("DeathZone"))
        {
            isDie = true;
           ChangeAnim("die");
           Invoke(nameof(OnInit), 0.5f);
        }
    }


}
