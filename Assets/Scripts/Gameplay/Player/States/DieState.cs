using UnityEngine;

public class DieState : BaseState
{
    public DieState(PlayerController player, PlayerStateMachine playerStateMachine, Animator animator) : base(player, playerStateMachine, animator)
    {
    }
    public override void Enter()
    {
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
