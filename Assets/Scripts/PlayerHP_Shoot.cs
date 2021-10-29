using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHP_Shoot : MonoBehaviour
{
    [SerializeField] private HealthBar heathBar;
    [SerializeField] private float healthAmount = 100, hpLossRatio = 5f;
    
    
    private float hpMax;

    private Damage playerDmg;
    void Start()
    {
        playerDmg = GetComponent<Damage>();
        hpMax = healthAmount;
    }
    
    void Shoot()
    {
        healthAmount -= Time.deltaTime * hpLossRatio;
        if (healthAmount <= 0)
        {
            healthAmount = 0;
        }
        if (healthAmount >= hpMax)
        {
            healthAmount = hpMax;
        }
        
        //heathBar.SetHealth(healthAmount / 100);
        float tHp = Mathf.InverseLerp(0f, hpMax, healthAmount);
        
        heathBar.SetHealth(tHp);
        //heathBar.SetHealthColor(tHp);
        
    }

    IEnumerator PlayerDies(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }
}
