using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField]
    private List<Item> items; // Список всех предметов в игре

    private Dictionary<string, Item> itemDictionary;

    private void Awake()
    {
        // Паттерн синглтона
        if (Instance == null)
        {
            Instance = this;
            InitializeItemDictionary();
        }
        else
        {
            Destroy(gameObject); // Удаляем дублирующий объект
        }
    }

    private void InitializeItemDictionary()
    {
        itemDictionary = new Dictionary<string, Item>();
        foreach (var item in items)
        {
            itemDictionary[item.id] = item; // Ключ - ID предмета, значение - сам предмет
        }
    }

    public Item GetItemById(string id)
    {
        if (itemDictionary.ContainsKey(id))
        {
            return itemDictionary[id];
        }
        else
        {
            Debug.LogError("Item with ID " + id + " not found.");
            return null;
        }
    }
}
