using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer = 0;
    // Start is called before the first frame update
    public void OnEnter(Enemy enemy)
    {
        if(enemy.Target != null)
        {
            // doi huong enemy toi huong cua player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            
            enemy.StopMoving();
            enemy.Attack();
           
        }
        timer = 0;
        
    }

    public void OnExcute(Enemy enemy)
    {
        timer+=Time.deltaTime;
        if(timer >= 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
        
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}