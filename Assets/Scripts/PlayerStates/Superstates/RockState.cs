using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockState : RootState
{
    public RockState(string stateName) : base(stateName)
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
        if (player.input.shovePlease)
        {
            player.input.UseShove();
            player.ChangeState(new RockBonkState("Bonk"));
        }

        if (player.input.pickPlease)
        {
            player.input.UsePick();
            player.ChangeState(new RockThrowState("Throw"));
        }
    }
}
