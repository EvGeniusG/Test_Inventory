using System;
using UnityEngine;


public class PlayerInventory : AInventory
{
    public AWallet wallet {get; private set;}

    public PlayerInventory(AWallet wallet){
        this.wallet = wallet;
    }
}