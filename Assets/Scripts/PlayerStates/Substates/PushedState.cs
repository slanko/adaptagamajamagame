using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedState : Downed
{
    private float force;
    private Vector3 dir;
    public PushedState(string stateName, float force, Vector3 dir) : base(stateName)
    {
        this.force = force;
        this.dir = dir;
    }

    public override void Enter(PlayerGod player)
    {
        base.Enter(player);
        player.Shove(force, dir);
    }

    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        player.ShoveLockout();
    }
}
