using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int myHp = 150;
    private int hpAux;
    
    public static float playerScore = 200;
    public int MyHp
    {
        get => myHp;
        set => myHp = value;
    }

    private void Start()
    {
        hpAux = myHp;
    }

    public int TakeDamage(int dmgAmount)
    {
        int hpLoss = myHp -= dmgAmount;
        if (hpLoss <= 0)
        {
            gameObject.SetActive(false);
            myHp = hpAux;
            switch (this.gameObject.tag)
            {
                case "Enemy":
                    ScoreSystem.Instance.MultiplyScore(200);
                    break;
            }
            
        }
        return hpLoss;
    }

    public void TakeDamage(int whatTakesDamage, int dmgAmount)
    {
        whatTakesDamage -= dmgAmount;
    }

    // private void OnEnable()
    // {
    //     if (this.CompareTag("Enemy"))
    //     {
    //         myHp = hpAux;
    //         print("Entered");
    //     }
    // }

    private void OnDisable()
    {
        
    }
}
