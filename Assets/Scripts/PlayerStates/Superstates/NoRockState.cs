using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRockState : RootState
{
    public NoRockState(string stateName) : base(stateName)
    {
    }

    public override void OnShoved(PlayerGod player, float force, Vector3 direction)
    {
        base.OnShoved(player, force, direction);
        player.ChangeState(new PushedState("Pushed", force, direction));
    }
}
