using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
public class BulletBehaviour : MonoBehaviour
{
    private Movement2D bulletMovement;
    private Damage bulletDmg;

    private int hpAmount = 10, hpMax;
    
    void Start()
    {
        bulletMovement = GetComponent<Movement2D>();
        bulletDmg = GetComponent<Damage>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            int hpLoss = bulletDmg.TakeDamage(10);
            if (hpLoss < 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
