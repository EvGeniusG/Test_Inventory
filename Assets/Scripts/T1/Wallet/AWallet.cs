
using System;
using UniRx;
using UnityEngine;


public class AWallet : ScriptableObject{

    public virtual IntReactiveProperty MoneyProperty { get; protected set; } = new IntReactiveProperty(0);
    public virtual int Money {
        get { return MoneyProperty.Value;}
        protected set { MoneyProperty.Value = value;}
    }

    public bool SpendMoney(int amount){
        if(Money >= amount){
            Money -= amount;
            return true;
        }
        return false;
    }

    public void AddMoney(int amount){
        Money += amount;
    }
}