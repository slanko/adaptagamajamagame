using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlaterData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Visual data")]
    public string characterName = "Traumin";
    public Sprite characterSprite = null;
    public GameObject characterMesh = null;
    public Material characterMaterial = null;

    [Header("Gameplay data")]
    public float speedMultiplier = 1;
    public float distanceMultiplier = 1;
}
