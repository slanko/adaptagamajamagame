using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickState : NoRockImobileState
{
    public PickState(string stateName) : base(stateName)
    {
    }

    public override void Enter(PlayerGod player)
    {
        base.Enter(player);
        player.BeginPickupLoop();
    }
    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        player.Stop();
    }
}
