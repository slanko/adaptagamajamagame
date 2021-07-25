using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fucked : RootState
{
    public Fucked(string stateName) : base(stateName)
    {
    }
    public override void Enter(PlayerGod player)
    {
        base.Enter(player);
        player.gameObject.tag = "Rock3";
        player.HUDDeath();
    }
}
