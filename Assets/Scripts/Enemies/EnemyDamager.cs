using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : Damage
{
    private int hpAux;
    private void OnEnable()
    {
        if (this.CompareTag("Enemy"))
        {
            MyHp = hpAux;
            print("Entered");
        }
    }

    private void OnDisable()
    {
        ScoreSystem.Instance.MultiplyScore(200);
    }
}
