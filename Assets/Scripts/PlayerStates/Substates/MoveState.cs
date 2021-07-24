using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : NoRockState
{
    public MoveState(string stateName) : base(stateName)
    {
    }

    public override void RegularUpdate(PlayerGod player)
    {
        base.RegularUpdate(player);
        if (player.moveVal == Vector2.zero)
        {
            player.ChangeState(new IdleState("Idle"));
        }
    }

    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        Debug.Log(player);
        player.Move();
    }

}
