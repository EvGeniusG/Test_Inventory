using UnityEngine;

[CreateAssetMenu()]
public class Item : ScriptableObject{
    [SerializeField] string _id;
    public string id => _id;

    [SerializeField] Sprite _itemIcon;
    public Sprite itemIcon => _itemIcon;

    [SerializeField] int _buyPrice;
    public int buyPrice => _buyPrice;

    [SerializeField] int _sellPrice;
    public int sellPrice => _sellPrice;
}