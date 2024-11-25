using System.Runtime;
using UniRx;
using UnityEngine;

public abstract class ADragAndDrop : MonoBehaviour
{
    public static ADragAndDrop Instance { get; protected set; }


    public ReactiveProperty<Item> DraggedItemProperty { get; protected set; } = new ReactiveProperty<Item>(null);
    public Item DraggedItem {
        get { return DraggedItemProperty.Value;}
        protected set { DraggedItemProperty.Value = value; }
    }

    protected InventoryController fromInventoryController;
    protected int fromCellIndex;

    protected InventoryController toInventoryController;
    protected int toCellIndex;

    
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Защита от создания нескольких экземпляров
            return;
        }
        Instance = this;
    }

    

    public abstract void StartDragging(InventoryController inventoryController, int fromCellIndex);
    public abstract void NewCellEntered(InventoryController inventoryController, int cellIndex);
    public abstract void CellExited(InventoryController inventoryController, int cellIndex);
}
