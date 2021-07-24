using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushState : NoRockImobileState
{
    public PushState(string stateName) : base(stateName)
    {
    }
    public override void Enter(PlayerGod player)
    {
        base.Enter(player);
        player.CommitViolenceSoftly();
    }

    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        player.Stop();
    }
}
