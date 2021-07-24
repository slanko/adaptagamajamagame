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

    public override void RegularUpdate(PlayerGod player)
    {
        base.RegularUpdate(player);
        if (player.input.shove)
        {
            player.input.useShove();
            player.ChangeState(new PushState("Push"));
        }
    }
}
