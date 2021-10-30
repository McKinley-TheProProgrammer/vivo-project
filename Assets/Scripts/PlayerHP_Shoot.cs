using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHP_Shoot : MonoBehaviour
{
    [SerializeField] private HealthBar heathBar;
    //Health amount is the player health itself
    [SerializeField] private float healthAmount = 100, hpLossRatio = 5f;
    public float GetHealthAmount() => healthAmount;
    
    private float hpMax;
    
    [SerializeField] float fireRate = .4f;
    
    private float fireAux;

    private Damage playerDmg;
    
    void Start()
    {
        playerDmg = GetComponent<Damage>();
        hpMax = healthAmount;
        playerDmg.MyHp = (int)healthAmount;
        
        fireAux = fireRate;
    }
    
    //Player loses Health as he is shooting
    void ShootHPLoss()
    {
        healthAmount -= Time.deltaTime * hpLossRatio;
        if (healthAmount <= 0)
        {
            healthAmount = 0;
            StartCoroutine(PlayerDies(1f));
        }
        if (healthAmount >= hpMax)
        {
            healthAmount = hpMax;
        }
        
        //heathBar.SetHealth(healthAmount / 100);
        float tHp = Mathf.InverseLerp(0f, hpMax, healthAmount);
        
        heathBar.SetHealth(tHp);
        //heathBar.SetHealthColor(tHp);
        playerDmg.MyHp = (int)healthAmount;
    }

    void ShootBullets()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0 && healthAmount != 0)
        {
            GameObject obj = Pooling.Instance.SpawnFromPool("BloodAmmo", transform.position, Quaternion.identity);
            fireRate = fireAux;
            print(playerDmg.MyHp);
        }
    }

    void BiteAttack()
    {
        
    }
    IEnumerator PlayerDies(float delay)
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(delay);
       
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ShootHPLoss();
            ShootBullets();
        }
    }
}
