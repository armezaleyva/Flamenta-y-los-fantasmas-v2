using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    [SerializeField]
    Potion potion;    
    [SerializeField]
    float speedBoost = 5f;
    [SerializeField]
    float speedSeconds = 3f;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            potion.Drink();
            Destroy(gameObject);

            other.GetComponent<Player>().SpeedBoostForSeconds(speedSeconds, speedBoost);
        }
    }
}