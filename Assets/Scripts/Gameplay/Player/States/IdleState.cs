using UnityEngine;
using MyGame.Core.Interface;
public class IdleState : BaseStatePlayer
{

    public IdleState(PlayerController player, FiniteStateMachine playerStateMachine, Animator animator) : base(player, playerStateMachine, animator){}


    public override void Enter()
    {
        animator.SetFloat("RunX", 0);
        animator.SetFloat("RunY", 0);
        animator.CrossFade("idle", 0.1f);
        Debug.Log("Trang thai dung yen");
    }

    public override void LogicUpdate()
    {
        if(player.MoveX!=0||player.MoveY!=0)
            {
                playerStateMachine.ChangeState(player.runState);
            }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void Exit()
    {
        Debug.Log("Thoai trang thai dung yen");
    }
}
