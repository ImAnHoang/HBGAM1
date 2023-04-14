using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 350;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttack;

    private float horizontal;

    private string currentAnim;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        // -1 --> 0 -->1
        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Debug.Log("Jump");
                Jump();
            }

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            
            {
                Attack();
            }

            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }


        }
        
        
        if (!isGrounded && rb.velocity.y < 0)
        {

            ChangeAnim("fall");
            isJumping = false;
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);

            // transform.localScale = new Vector3(horizontal, 1, 1);

            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
        


    }

    private bool CheckGrounded()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }

    private void Attack()
    {
        ChangeAnim("attack");
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void Throw()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
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

}

