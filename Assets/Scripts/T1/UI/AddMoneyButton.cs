using UnityEngine;

public class AddMoneyButton : MonoBehaviour{
    [SerializeField] AWallet wallet;

    public void Tap(){
        wallet.AddMoney(100);
    }
}