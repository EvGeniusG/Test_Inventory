using System;
using UnityEngine;

public class PlayerInventoryController : InventoryController
{
    [SerializeField] AWallet playerWallet;
    private PlayerInventory inventory;

    public override AInventory Inventory => inventory;

    protected override void CreateDefaultInventory()
    {
        var defaultInventory = new PlayerInventory(playerWallet);
        defaultInventory.InitializeFromData(defaultInventoryData); // Заполняем из ScriptableObject
        inventory = defaultInventory;
        Inventory.OnItemsChanged.Execute();
    }

    protected override void LoadInventoryFromPrefs()
    {
        inventory = new PlayerInventory(playerWallet);
        inventory.FromJson(PlayerPrefs.GetString(inventoryPrefsKey));
        Inventory.OnItemsChanged.Execute();
    }

    public override bool CanBuy(Item item, int index)
    {
        if(inventory.Items.ContainsKey(index)) return false;
        if(inventory.wallet.Money < item.buyPrice) return false;
        return true;
    }

    public override void BuyItem(Item item, int index)
    {
        if(!CanBuy(item, index)) {
            Debug.LogError("Player cannot buy item!");
            return;
        }

        inventory.wallet.SpendMoney(item.buyPrice);
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

        Item item = inventory.Items[index];
        inventory.wallet.AddMoney(item.sellPrice);
        inventory.Items.Remove(index);
        Inventory.OnItemsChanged.Execute();
    }

    protected override void SaveInventory()
    {
        PlayerPrefs.SetString(inventoryPrefsKey, inventory.ToJson());
    }
}
