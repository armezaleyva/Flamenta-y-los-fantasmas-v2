using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Items/Potion", order = 1)]
public class Potion : Item 
{
    [SerializeField]
    AudioClip soundEffect;
    public void Drink()
    {
        AudioSource.PlayClipAtPoint(soundEffect, new Vector3(0, 0, 0));
    }
}
