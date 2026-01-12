using MyGame.Core.Interface;
using UnityEngine;
public class PlayerStateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    public void Intialize(IState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        if (CurrentState == newState) return;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
 
}