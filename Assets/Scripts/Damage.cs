using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int myHp;
    public int MyHp
    {
        get => myHp;
        set => myHp = value;
    }
    
    public int TakeDamage(int dmgAmount)
    {
        int hpLoss = myHp -= dmgAmount;
        if (hpLoss <= 0)
        {
            gameObject.SetActive(false);
        }
        return hpLoss;
    }

    public void TakeDamage(int whatTakesDamage, int dmgAmount)
    {
        whatTakesDamage -= dmgAmount;
    }
    
    
}
