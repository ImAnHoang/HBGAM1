using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody2D rb;
    // [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;

    [SerializeField] private Kunai kunaiPrefab;

    [SerializeField] private Transform thorwPoint;

    [SerializeField] private GameObject attackArea;

    
    
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;

    // private bool isDie = false;
    // private string currentAnim;
    private float horizontal;

    private int coin = 0;

    private Vector3 savePoint;
    [SerializeField] private float jumpForce = 350;
    
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(IsDead)
        {
            return;
        }
        isGrounded = CheckGrounded();

        // -1 --> 0 --> 1
        // horizontal = Input.GetAxisRaw("Horizontal");
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
            rb.velocity = new Vector2(horizontal  * speed, rb.velocity.y);

            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }


    }

    public override void OnInit()
    {

        base.OnInit();
        // isDie = false;
        isAttack = false;

        transform.position = savePoint;

        ChangeAnim("idle");
        DeActiveAttack();
        SavePoint();

        UIManager.instance.UpdateCoinText(coin);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();

    }

    protected override void OnDeath()
    {
        base.OnDeath();
        
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


    public void Attack()
    {
        
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);

        
    }
    public void Throw()
    {
       
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);

        Instantiate(kunaiPrefab, thorwPoint.position, thorwPoint.rotation);
    }

    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("ilde");
    }
  

    public void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    // private void ChangeAnim(string animName)
    // {
    //     if (currentAnim != animName)
    //     {
    //         anim.ResetTrigger(animName);
    //         currentAnim = animName;
    //         anim.SetTrigger(currentAnim);
    //     }

    // }
    // Active Attack Area
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.UpdateCoinText(coin);
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("DeathZone"))
        {
            // isDie = true;
           ChangeAnim("die");
           Invoke(nameof(OnInit), 0.5f);
        }
        if(collision.CompareTag("HP_Item"))
        {
            if(hp < healthbar.GetMaxHp())
            {
                hp += 30;
                healthbar.SetNewHp(hp);
                Destroy(collision.gameObject);
            } else{
                return;
            }
        }
        
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }

}
