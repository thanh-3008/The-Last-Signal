using UnityEngine;

public class DieState : BaseStatePlayer
{
    public DieState(PlayerController player, FiniteStateMachine playerStateMachine, Animator animator) : base(player, playerStateMachine, animator)
    {
    }
    public override void Enter()
    {
        // Dừng mọi chuyển động vật lý ngay lập tức
        player.rb.linearVelocity = Vector2.zero;

        // Reset các biến đầu vào để animator không bị nhầm lẫn
        animator.SetFloat("RunX", 0);
        animator.SetFloat("RunY", 0);
        animator.Play("die");
        GameTimeManager.Instance.SetPlayerDead(true);
    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    {       
    }

    public override void Exit()
    {
    }
}
