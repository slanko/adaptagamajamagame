using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowState : RockImobileState
{
    public RockThrowState(string stateName) : base(stateName)
    {
    }
    public override void Enter(PlayerGod player)
    {
        base.Enter(player);
        player.BeginThrowLoop();
    }
    public override void PhysicsUpdate(PlayerGod player)
    {
        base.PhysicsUpdate(player);
        player.Stop();
    }
}
