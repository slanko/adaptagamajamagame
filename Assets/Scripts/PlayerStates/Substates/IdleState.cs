using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : NoRockState
{
    public IdleState(string stateName) : base(stateName)
    {
    }

    public override void RegularUpdate(PlayerGod player)
    {
        base.RegularUpdate(player);
        if (player.input.moveVals != Vector2.zero)
        {
            player.ChangeState(new MoveState("Move"));
        }
    }
    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        player.Stop();
    }

}
