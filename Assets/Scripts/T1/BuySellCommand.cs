using System.Data;
using UniRx.Triggers;

public static class BuySellCommand{
    public static void Execute(
        InventoryController buyer, 
        InventoryController seller,
        int buyerInventoryIndex,
        int sellerInventoryIndex){

        if(buyer == seller){
            buyer.ReplaceItems(buyerInventoryIndex, sellerInventoryIndex);
            return;
        }
        Item item;
        if(!seller.Inventory.Items.TryGetValue(sellerInventoryIndex, out item)) return;
        if(!seller.CanSell(sellerInventoryIndex)) return;
        if(!buyer.CanBuy(item, buyerInventoryIndex)) return;

        seller.SellItem(sellerInventoryIndex);
        buyer.BuyItem(item, buyerInventoryIndex);
    }
}