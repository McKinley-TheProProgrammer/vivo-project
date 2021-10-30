using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
public class BulletBehaviour : MonoBehaviour, IHit
{
    private Movement2D bulletMovement;
    
    [SerializeField] private int bulletDmgAmount = 10;
    private Damage bulletDmg;

    private int hpAmount = 10, hpMax;
    
    void Start()
    {
        bulletMovement = GetComponent<Movement2D>();
        bulletDmg = GetComponent<Damage>();
    }

    public void BulletMovement()
    {
        bulletMovement.Move(0f, 5, false);
    }

    public void Bullet_NormalDMG()
    {
        this.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Damage>().TakeDamage(bulletDmgAmount);
            OnHit();
        }
    }

    public void OnHit()
    {
        Bullet_NormalDMG();
    }
    private void FixedUpdate()
    {
        BulletMovement();
    }
}
