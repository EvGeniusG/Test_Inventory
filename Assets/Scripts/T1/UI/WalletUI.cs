using TMPro;
using UniRx;
using UnityEngine;
public class WalletUI : MonoBehaviour{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] AWallet wallet;
    void Start(){
        wallet.MoneyProperty.Subscribe(money => {
            moneyText.text = money.ToString();
        }).AddTo(this);
    }
}