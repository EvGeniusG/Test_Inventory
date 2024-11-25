using System;
using UniRx;
using UnityEngine;

public abstract class InventoryController : MonoBehaviour
{
    [SerializeField] protected DefaultInventoryData defaultInventoryData; // ScriptableObject для дефолтных данных

    [SerializeField] protected string inventoryPrefsKey;
    public ReactiveCommand OnInventoryInit {get; protected set; } = new ReactiveCommand();
    protected virtual void Start()
    {
        InitializeInventory();

        Inventory.OnItemsChanged.Subscribe( _ =>{
            SaveInventory();
        }).AddTo(this);

        OnInventoryInit.Execute();
    }

    private void InitializeInventory()
    {
        if (PlayerPrefs.HasKey(inventoryPrefsKey))
        {
            LoadInventoryFromPrefs();
        }
        else 
        if (defaultInventoryData != null)
        {
            // Если нет префаба, заполняем дефолтные данные
            CreateDefaultInventory();
        }
        else
        {
            throw new Exception("No inventory prefab or default data provided!");
        }
    }
    public abstract AInventory Inventory { get; }
    protected abstract void LoadInventoryFromPrefs();
    protected abstract void CreateDefaultInventory(); // Метод для создания дефолтного инвентаря

    public void ReplaceItems(int firstIndex, int secondIndex)
    {
        Item item0, item1;
        bool firstItemIsReady = Inventory.Items.TryGetValue(firstIndex, out item0);
        bool secondItemIsReady = Inventory.Items.TryGetValue(secondIndex, out item1);
        if(firstItemIsReady){
            Inventory.Items.Remove(secondIndex);
            Inventory.Items.Add(secondIndex, item0);
        }
        else{
            Inventory.Items.Remove(secondIndex);
        }
        if(secondItemIsReady){
            Inventory.Items.Remove(firstIndex);
            Inventory.Items.Add(firstIndex, item1); 
        }
        else{
            Inventory.Items.Remove(firstIndex);
        }
        Inventory.OnItemsChanged.Execute();
    }


    public abstract bool CanBuy(Item item,int index);
    public abstract bool CanSell(int index);
    public abstract void SellItem(int index);
    public abstract void BuyItem(Item item, int index);

    protected abstract void SaveInventory();

}