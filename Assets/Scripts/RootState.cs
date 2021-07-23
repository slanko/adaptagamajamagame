using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootState
{
    protected float startTime;
    private string stateName;
    public RootState(string stateName)
    {
        this.stateName = stateName;
    }

    public virtual void Enter(PlayerGod player)
    {
        startTime = Time.time;
        Debug.Log($"Player state is now " + stateName);
    }

    public virtual void RegularUpdate(PlayerGod player)
    {

    }

    public virtual void PhysicsUpdate(PlayerGod player)
    {

    }
    
    public virtual void Exit(PlayerGod player)
    {

    }

}
