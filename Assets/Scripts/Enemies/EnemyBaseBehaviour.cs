using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseBehaviour : MonoBehaviour
{
    public float dmgAmount = 10;
    private Damage enemyDamage;
    public abstract void DoAttack();

    private void Start()
    {
        enemyDamage = GetComponent<Damage>();
    }
}
