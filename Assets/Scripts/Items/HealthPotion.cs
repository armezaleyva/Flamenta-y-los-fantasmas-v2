using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField]
    Potion potion;    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            potion.Drink(player);
            Destroy(gameObject);

            player.HealPlayer();
        }
    }
}