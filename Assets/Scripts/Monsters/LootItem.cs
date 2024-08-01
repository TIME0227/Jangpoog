using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    HPpotionLargePrefab,
    HPpotionSmallPrefab,
    InvisibilityPotionPrefab,
    MPpotionLargePrefab,
    MPpotionSmallPrefab
}

[System.Serializable]
public class LootItem
{
    public Items item;
    [Range(0, 100)] public float dropChance;
    
}
