using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class PlayerContext : Contexts.IContext
{
    private IState _currentState;

    public PlayerContext(IState initialState)
    {
        _currentState = initialState;
    }

    public static bool PlayerRightArrowDown
    {
        get { return Input.GetButtonDown("Right"); }
    }

    public IState CurrentState => _currentState;
    
    public void ChangeState(IState state)
    {
        _currentState.OnEnter(this);
        _currentState = state;
        _currentState.OnExit(this);
    }
}
