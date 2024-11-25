using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DraggedItemUI : MonoBehaviour
{
    [SerializeField] private Image draggedItemImage; // UI-элемент для отображения спрайта
    [SerializeField] private Canvas canvas; // Canvas для правильного позиционирования

    private void Start()
    {
        // Подписываемся на изменения DraggedItem
        ADragAndDrop.Instance.DraggedItemProperty
            .Subscribe(OnDraggedItemChanged)
            .AddTo(this);
    }

    private void Update()
    {
        MoveDraggedItem();
    }

    private void OnDraggedItemChanged(Item draggedItem)
    {
        if (draggedItem != null)
        {
            draggedItemImage.color = Color.white;
            // Устанавливаем спрайт и отображаем иконку
            draggedItemImage.sprite = draggedItem.itemIcon;
            draggedItemImage.enabled = true;
            MoveDraggedItem();
        }
        else
        {
            // Скрываем иконку, если ничего не перетаскивается
            draggedItemImage.color = Color.clear;
            draggedItemImage.enabled = false;
        }
    }

    private void MoveDraggedItem(){
        if (draggedItemImage.enabled)
        {
            // Обновляем позицию иконки, следуя за мышью
            Vector2 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, 
                mousePosition, 
                canvas.worldCamera, 
                out var localPoint);
            draggedItemImage.rectTransform.anchoredPosition = localPoint;
        }
    }
}
