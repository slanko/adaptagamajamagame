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

    public override void OnBonked(PlayerGod player, float magnitude, Vector3 direction, int rockLevel)
    {
        base.OnBonked(player, magnitude, direction, rockLevel);
        if(magnitude >= 4 && rockLevel >= 2)
        {
            player.ChangeState(new BonkedState("Bonked", rockLevel, direction));
        }
        else if(magnitude >= 4)
        {
            player.ChangeState(new PushedState("Pushed", magnitude * rockLevel * 2, direction));
        }
    }

    public override void RegularUpdate(PlayerGod player)
    {
        base.RegularUpdate(player);
        if (player.input.shovePlease)
        {
            player.input.UseShove();
            player.ChangeState(new PushState("Push"));
        }

        if (player.input.pickPlease)
        {
            player.input.UsePick();
            player.ChangeState(new PickState("Pickup"));
        }
    }
}
