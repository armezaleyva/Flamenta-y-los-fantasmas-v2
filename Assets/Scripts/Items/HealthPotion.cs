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
            potion.Drink();
            Destroy(gameObject);

            other.GetComponent<Player>().HealPlayer();
        }
    }
}