using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBonkState : RockState
{
    public RockBonkState(string stateName) : base(stateName)
    {
    }
    public override void Enter(PlayerGod player)
    {
        base.Enter(player);
        player.CommitViolenceViolently();
    }

    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        player.Move();
    }
}
