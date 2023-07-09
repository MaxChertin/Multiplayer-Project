using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Texture2D icon;
}
