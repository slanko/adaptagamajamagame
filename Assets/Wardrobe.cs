using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] private PlayerData targetCharacter, altCharacter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.GetComponent<PlayerGod>().altCharacter && altCharacter != null) other.GetComponent<PlayerGod>().ChangePlayerData(altCharacter);
            else other.GetComponent<PlayerGod>().ChangePlayerData(targetCharacter);
        }
    }
}
