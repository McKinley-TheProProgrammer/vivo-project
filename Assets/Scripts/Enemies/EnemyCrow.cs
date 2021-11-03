using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrow : EnemyBaseBehaviour , IPool
{

    [SerializeField] private float radius = 1f;
    
    //[SerializeField] private LayerMask playerLayer;
    private GameObject player;
    
    
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyDamage = GetComponent<Damage>();
    }

    public override void DoAttack()
    {
        bool hittedSomthing = Physics2D.OverlapCircle(transform.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHP_Shoot>().SubstractHealthAmount(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHP_Shoot>().SubstractHealthAmount(20);
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(FlashHit(spriteRenderer, .08f));
        }
    }

    public void OnSpawnedObject()
    {
        print("Entered");
        spriteRenderer.color = Color.white;
    }
}
