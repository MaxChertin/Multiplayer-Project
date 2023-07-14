using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    VeryRare,
    Epic,
    Legendary,
    Extreme
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public new string name { get; private set; } = "New Item";
    [field: SerializeField] public Sprite icon { get; private set; } = null;
    [field: SerializeField] public uint id { get; private set; } = 0;
    [field: SerializeField] public bool isStackable { get; private set; } = true;
    [field: SerializeField] public Rarity rarity { get; private set; } = Rarity.Common;
}
