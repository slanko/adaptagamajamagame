using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockIdleState : RockState
{
    public RockIdleState(string stateName) : base(stateName)
    {
    }

    public override void RegularUpdate(PlayerGod player)
    {
        base.RegularUpdate(player);
        if (player.input.moveVals != Vector2.zero)
        {
            player.ChangeState(new RockMoveState("RockMove"));
        }
    }
    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        player.Stop();
    }
}
