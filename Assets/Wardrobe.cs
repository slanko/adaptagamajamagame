using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] private PlayerData targetCharacter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerGod>().ChangePlayerData(targetCharacter);
        }
    }
}
