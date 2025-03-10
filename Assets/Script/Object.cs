using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Throwable
}

[CreateAssetMenu(fileName = "Item", menuName = " New Item")]
public class Object : ScriptableObject
{
    public Sprite icon;
    public string name;
    public string description;
    public ItemType type;

    [Header("Stacking")]
    public bool canStack;
    public int maxStack;
}
