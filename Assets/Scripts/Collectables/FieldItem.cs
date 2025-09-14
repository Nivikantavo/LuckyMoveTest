using UnityEngine;

public enum ItemType
{
    Coin,
    Shield
}

public class FieldItem
{
    public ItemType Type { get; set; }

    public FieldItem(ItemType type)
    {
        Type = type;
    }
}