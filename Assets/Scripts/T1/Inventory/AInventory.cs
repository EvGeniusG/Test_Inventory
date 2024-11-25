using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public abstract class AInventory
{
    public ReactiveProperty<Dictionary<int, Item>> ItemsProperty {get; private set;} = new ReactiveProperty<Dictionary<int,Item>>(new Dictionary<int, Item>());
    public Dictionary<int, Item> Items{
        get { return ItemsProperty.Value;}
        private set { ItemsProperty.Value = value;}
    }
    public ReactiveCommand OnItemsChanged {get; private set;} = new ReactiveCommand();

    public virtual void InitializeFromData(DefaultInventoryData data)
    {
        Items.Clear();
        for (int i = 0; i < data.Items.Count; i++)
        {
            Items[i] = data.Items[i];
        }
        OnItemsChanged.Execute();
    }

    public virtual string ToJson()
    {
        SerializableInventory serializableInventory = new SerializableInventory();

        foreach (var pair in Items)
        {
            serializableInventory.indexes.Add(pair.Key);
            serializableInventory.items.Add(pair.Value.id); // Сохраняем только ID предметов
        }

        return JsonUtility.ToJson(serializableInventory);
    }

    public virtual void FromJson(string json)
    {
        SerializableInventory serializableInventory = JsonUtility.FromJson<SerializableInventory>(json);
        Items.Clear();

        for (int i = 0; i < serializableInventory.indexes.Count; i++)
        {
            Item item = ItemDatabase.Instance.GetItemById(serializableInventory.items[i]); // Загружаем предмет по ID
            if (item != null)
            {
                Items.Add(serializableInventory.indexes[i], item);
            }
        }
        OnItemsChanged.Execute();
    }
}


[Serializable]
public class SerializableInventory
{
    public List<int> indexes = new List<int>();
    public List<string> items = new List<string>();
}
