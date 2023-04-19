using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    [SerializeField] private Animator anim;

    private float hp;
    public bool IsDead => hp <= 0;
    
    private string currentAnim;

    void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 100;

    }
    public virtual void OnDespawn()
    {
        
    }
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);

    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }

    }

    public virtual void OnHit( float damage )
    {
        Debug.Log("OnHit");
       if(!IsDead)
       {
           hp -= damage;
           if(IsDead)
           {
               OnDeath();
           }

       }
       
    }
    
    
    
  
}
