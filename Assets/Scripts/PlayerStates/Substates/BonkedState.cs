using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkedState : Downed
{
    private float rockLevel;
    private Vector3 dir;
    public BonkedState(string stateName, float rockLevel, Vector3 dir) : base(stateName)
    {
        this.rockLevel = rockLevel;
        this.dir = dir;
    }

    public override void Enter(PlayerGod player)
    {
        base.Enter(player);

        player.Bonked(rockLevel, dir);
    }
    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);

        player.BonkedFixedupdate();
    }
}
