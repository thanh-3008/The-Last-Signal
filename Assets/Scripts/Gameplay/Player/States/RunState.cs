using MyGame.Core.Interface;
using UnityEngine;

public class RunState : BaseState
{
    public RunState(PlayerController player,PlayerStateMachine playerStateMachine,Animator animator) : base(player, playerStateMachine, animator) { }
    public override void Enter()
    {       
        Debug.Log("Trang thai di chuyen");
    }

    public override void LogicUpdate()
    {
        if(player.MoveX==0 && player.MoveY==0)
        {
            playerStateMachine.ChangeState(player.idleState);
        }
    }
    public override void PhysicsUpdate()
    {

        Vector2 move = new Vector2(player.MoveX, player.MoveY);

        if(Mathf.Abs(move.x)>0)
        {
            animator.SetFloat("RunX", Mathf.Abs(move.x));
            animator.SetFloat("RunY", 0);
            if (move.x>0)
            {
                player.transform.localScale = new Vector3(-1,1,1);
            }
            else if(move.x<0) 
            {
                player.transform.localScale = Vector3.one;
            }
            
        }
        else if (player.MoveX == 0 && Mathf.Abs(player.MoveY) > 0)
        {
          
            animator.SetFloat("RunY", move.y);
            animator.SetFloat("RunX", 0);
        }

        player.Move(move);
    }

    public override void Exit()
    {
        player.Idle(Vector2.zero);

        Debug.Log("Thoat trang thai di chuyen");
    }


}
