using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public new string name { get; private set; } = "New Item";
    [field: SerializeField] public Sprite icon { get; private set; } = null;
    [field: SerializeField] public ushort id { get; private set; } = 0;
    [field: SerializeField] public bool isStackable { get; private set; } = true;
}
