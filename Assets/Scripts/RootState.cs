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
        player.anim.SetBool(stateName, true);
        startTime = Time.time;
        Debug.Log($"Player state is now " + stateName);
    }

    public virtual void RegularUpdate(PlayerGod player)
    {
        if(player.transform.position.y <= player.lavaYLevel)
        {
            player.HUDDeath();
        }
    }

    public virtual void PhysicsUpdate(PlayerGod player)
    {

    }
    
    public virtual void Exit(PlayerGod player)
    {
        player.anim.SetBool(stateName, false);
    }

    public virtual void OnBonked(PlayerGod player, float magnitude, Vector3 direction, int rockLevel)
    {
    }

    public virtual void OnShoved(PlayerGod player, float force, Vector3 direction)
    {
    }

}
