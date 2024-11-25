using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemCell[] itemCells;

    [SerializeField] InventoryController inventoryController;


    void Start(){
        inventoryController.OnInventoryInit.Take(1)
            .Subscribe(_ => {
                for (int i = 0; i < itemCells.Length; i++){
                    itemCells[i].InitCell(inventoryController, i);
                }
                inventoryController.Inventory.OnItemsChanged.Execute();
            }).AddTo(this);
    }
}
