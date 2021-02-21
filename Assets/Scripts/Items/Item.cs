using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject 
{
    [SerializeField]
    Sprite icon;
    
    [SerializeField]
    protected string objectName = "Potion";
}
