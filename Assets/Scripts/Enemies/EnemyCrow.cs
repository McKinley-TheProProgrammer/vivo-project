using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrow : EnemyBaseBehaviour
{

    [SerializeField] private float radius = 1f;
    void Start()
    {
        
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
    }

    void Update()
    {
        
    }
}
