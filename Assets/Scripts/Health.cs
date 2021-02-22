using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Player player;
    [SerializeField]
    Image[] hearts;
    [SerializeField]
    Sprite fullHeart;
    [SerializeField]
    Sprite emptyHeart;

    void Awake() 
    {
        player = GetComponent<Player>();    
    }
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.hp)
            {
                hearts[i].sprite = fullHeart;
            }
            else 
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
