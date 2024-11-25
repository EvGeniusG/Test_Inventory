using UniRx;
using UnityEngine;

[CreateAssetMenu]
public class Wallet : AWallet{
    [SerializeField] private string moneyPrefsKey;
    private IntReactiveProperty _moneyProperty;
    public override IntReactiveProperty MoneyProperty{
        get{
            if(_moneyProperty == null){
                _moneyProperty = new IntReactiveProperty(PlayerPrefs.GetInt(moneyPrefsKey, 0));
            }
            return _moneyProperty;
        }
        protected set{
            _moneyProperty = value;
        }
    }

    public override int Money{
        get { return MoneyProperty.Value;}
        protected set { 
            MoneyProperty.Value = value;
            PlayerPrefs.SetInt(moneyPrefsKey, value);
        }
    }
}