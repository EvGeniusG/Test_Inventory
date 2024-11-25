using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultInventoryData", menuName = "Inventory/Default Data")]
public class DefaultInventoryData : ScriptableObject
{
    [SerializeField] private List<Item> _items;
    public List<Item> Items => _items;
}
