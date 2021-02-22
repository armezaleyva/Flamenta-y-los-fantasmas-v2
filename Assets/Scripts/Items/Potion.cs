using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Items/Potion", order = 1)]
public class Potion : Item 
{
    [SerializeField]
    AudioClip soundEffect;
    public void Drink(Player player)
    {
        Vector3 playerPosition = player.transform.position;
        AudioSource.PlayClipAtPoint(soundEffect, playerPosition, 1f);
    }
}
