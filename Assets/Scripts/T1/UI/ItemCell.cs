using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;

    private Item currentItem;

    private InventoryController inventoryController;
    private int cellIndex;

    private BoolReactiveProperty isDragging = new BoolReactiveProperty(false);


    public void InitCell(InventoryController inventoryController, int cellIndex){
        this.inventoryController = inventoryController;
        this.cellIndex = cellIndex;

        inventoryController.Inventory.OnItemsChanged
            .Subscribe(_ => {
            var items = inventoryController.Inventory.Items;
            Item newItem;
            if(items.TryGetValue(cellIndex, out newItem)){
                SetItem(newItem);
            }
            else{
                ClearItem();
            }

            
        }).AddTo(this);

        isDragging.Subscribe(value => {
            itemIcon.enabled = !value;
        }).AddTo(this);

    }

    // Устанавливаем предмет в клетку
    private void SetItem(Item item)
    {
        currentItem = item;
        itemIcon.sprite = item.itemIcon;
        itemIcon.enabled = true;
    }

    // Очищаем клетку
    private void ClearItem()
    {
        currentItem = null;
        itemIcon.enabled = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            ADragAndDrop.Instance.StartDragging(inventoryController, cellIndex);
            isDragging.Value = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem == null)
        {
            ADragAndDrop.Instance.NewCellEntered(inventoryController, cellIndex);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentItem == null)
        {
            ADragAndDrop.Instance.CellExited(inventoryController, cellIndex);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging.Value = false;
    }
}
