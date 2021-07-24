using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFinder : MonoBehaviour
{
    [SerializeField] private PlayerGod player;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Rock")
        {
            player.pickBoxTarget = other.transform;
        }
        else
        {
            player.pickBoxTarget = null;
        }
    }
}
