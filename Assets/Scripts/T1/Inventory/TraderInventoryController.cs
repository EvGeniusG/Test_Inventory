using System;
using UnityEngine;

public class TraderInventoryController : InventoryController
{

    private TraderInventory inventory;
    public override AInventory Inventory => inventory;

    protected override void CreateDefaultInventory()
    {
        var defaultInventory = new TraderInventory();
        defaultInventory.InitializeFromData(defaultInventoryData); // Заполняем из ScriptableObject
        inventory = defaultInventory;
        Inventory.OnItemsChanged.Execute();
    }

    protected override void LoadInventoryFromPrefs()
    {
        inventory = new TraderInventory();
        inventory.FromJson(PlayerPrefs.GetString(inventoryPrefsKey));
        Inventory.OnItemsChanged.Execute();
    }

    public override bool CanBuy(Item item, int index)
    {
        if(inventory.Items.ContainsKey(index)) return false;
        return true;
    }
    public override void BuyItem(Item item, int index)
    {
        if(!CanBuy(item, index)) {
            Debug.LogError("Trader cannot buy item!");
            return;
        }
        inventory.Items.Add(index, item);
        Inventory.OnItemsChanged.Execute();
    }

    public override bool CanSell(int index)
    {
        if(!inventory.Items.ContainsKey(index)) return false;
        return true;
    }


    public override void SellItem(int index)
    {
        if(!CanSell(index)){
            Debug.LogError("Trader cannot sell item!");
            return;
        }
        inventory.Items.Remove(index);
        Inventory.OnItemsChanged.Execute();
    }

    protected override void SaveInventory()
    {
        PlayerPrefs.SetString(inventoryPrefsKey, inventory.ToJson());
    }
}
