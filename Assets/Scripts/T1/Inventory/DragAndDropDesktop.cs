using UnityEngine;
using UniRx;

public class DragAndDropDesktop : ADragAndDrop
{

    private void OnEnable()
    {
        // Подписываемся на отжатие мыши
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0)) // Или Touch.phase == Ended для тач-устройств
            .Subscribe(_ => OnDragEnd())
            .AddTo(this);
    }

    public override void CellExited(InventoryController inventoryController, int cellIndex)
    {
        if(toInventoryController == inventoryController && toCellIndex == cellIndex)
        {
            toInventoryController = null;
        }
    }

    public override void NewCellEntered(InventoryController inventoryController, int cellIndex)
    {
        toInventoryController = inventoryController;
        toCellIndex = cellIndex;
    }

    public override void StartDragging(InventoryController inventoryController, int cellIndex)
    {
        fromInventoryController = inventoryController;
        fromCellIndex = cellIndex;
        DraggedItem = inventoryController.Inventory.Items[cellIndex];

        //Нужно подписаться на отжатие мыши/конец тапа, чтобы воспользоваться методом BuySellCommande.Execute
    }

     private void OnDragEnd()
    {
        if (DraggedItem == null)
            return; 

        if (toInventoryController != null)
        {
            // Выполняем команду покупки/продажи
            BuySellCommand.Execute(toInventoryController, fromInventoryController, toCellIndex, fromCellIndex);
        }

        // Сбрасываем состояние
        ResetDragState();
    }

    private void ResetDragState()
    {
        DraggedItem = null;
        fromInventoryController = null;
        toInventoryController = null;
        fromCellIndex = -1;
        toCellIndex = -1;
    }
}
