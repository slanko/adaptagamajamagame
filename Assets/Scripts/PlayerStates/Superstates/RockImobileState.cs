using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockImobileState : RootState
{
    public RockImobileState(string stateName) : base(stateName)
    {
    }
    public override void OnBonked(PlayerGod player, float magnitude, Vector3 direction, int rockLevel)
    {
        base.OnBonked(player, magnitude, direction, rockLevel);
        player.DropRock();
        if (magnitude >= 4 && rockLevel >= 2)
        {
            player.DropRock();
            player.ChangeState(new BonkedState("Bonked", rockLevel, direction));
        }
        else if (magnitude >= 4)
        {
            player.DropRock();
            player.ChangeState(new PushedState("Pushed", magnitude * rockLevel * 2, direction));
        }
    }
}
