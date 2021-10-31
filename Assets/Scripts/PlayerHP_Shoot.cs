using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHP_Shoot : MonoBehaviour
{
    [SerializeField] private HealthBar heathBar;
    //Health amount is the player health itself
    [SerializeField] private float healthAmount = 100, hpLossRatio = 5f;
    public float GetHealthAmount() => healthAmount;
    public float SubstractHealthAmount(float healthAmount) => this.healthAmount -= healthAmount;
    
    private float hpMax;
    
    [SerializeField] float fireRate = .4f;
    
    private float fireAux;

    [SerializeField] private Transform bitePoint;
    [SerializeField] private float biteRange = 1;
    [SerializeField] private LayerMask whoIsGettingBit;

    private Damage playerDmg;

    [SerializeField] TextMeshProUGUI deathCountdownText;
    
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
            startDeathCount = true;
            healthAmount = 0;
        }
        else
        {
            startDeathCount = false;
            //print("Enter");
            //StopCoroutine(PlayerDies(3));
            //StopAllCoroutines();
        }
        if (healthAmount >= hpMax)
        {
            healthAmount = hpMax;
        }
        
        //heathBar.SetHealth(healthAmount / 100);
        float tHp = Mathf.InverseLerp(0f, hpMax, healthAmount);
        
        heathBar.SetHealth(tHp);
    
        playerDmg.MyHp = (int)healthAmount;
    }

    void ShootBullets()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0 && healthAmount != 0)
        {
            GameObject obj = Pooling.Instance.SpawnFromPool("BloodAmmo", transform.position, Quaternion.identity);
            fireRate = fireAux;
            //print(playerDmg.MyHp);
            AudioManager.Instance.Play("ShootSFX");
        }
    }

    void BiteAttack()
    {
        //bool checkLifeBat = Physics2D.Raycast(bitePoint.position, Vector2.up,biteRange, whoIsGettingBit);
        Collider2D checkLifeBat = Physics2D.OverlapCircle(bitePoint.position, biteRange, whoIsGettingBit);
        //bool checkLifeBat = Physics2D.OverlapCircle(bitePoint.position, biteRange, whoIsGettingBit);
        if (checkLifeBat.CompareTag("LifeBat"))
        {
            healthAmount += 30;
            checkLifeBat.gameObject.SetActive(false);
            GameObject vfxParticle =
                Pooling.Instance.SpawnFromPool("LifeBatVFX", bitePoint.position, bitePoint.rotation);
            
            //vfxParticle.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            healthAmount += 0;
        }
    }

    #region Death Implements

    private bool startDeathCount;
    private float deathCountdown = 3;
    void StartDeathCountDown()
    {
        if (startDeathCount)
        {
            deathCountdownText.gameObject.SetActive(true);
            deathCountdown -= Time.deltaTime;
            int d = (int)deathCountdown;
            deathCountdownText.text = d.ToString();
            if (deathCountdown <= 0)
            {
                deathCountdown = 0;
                StartCoroutine(PlayerDies(3));
                startDeathCount = false;
            }
        }
        else
        {
            deathCountdown = 3;
            deathCountdownText.gameObject.SetActive(false);
        }
        
    }
    
    void CacheDeath()
    {
        GetComponent<Movement2D>().GetRigidBody2D().gravityScale = 2f;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }
    
    IEnumerator PlayerDies(float deathDelay)
    {
        // startDeathCount = true;
        // //yield return new WaitForSeconds(deathDelay);
        // yield return new WaitUntil(() => deathCountdown <= 0);
        CacheDeath();
        yield return new WaitForSeconds(1f);
        StartCoroutine(GameManager.EndGame(1f));
    }

    #endregion

    private bool playerDead;
    void Update()
    {
        float tHp = Mathf.InverseLerp(0f, hpMax, healthAmount);
        heathBar.SetHealth(tHp);
        
        if (healthAmount <= 0 && !playerDead)
        {
            StartCoroutine(PlayerDies(1));
            playerDead = true;
           
        }
        StartDeathCountDown();
        if (Input.GetKey(KeyCode.Space))
        {
            ShootHPLoss();
            ShootBullets();
        }
        
       

        if (Input.GetKeyDown(KeyCode.F))
        {
            BiteAttack();
        }
        
    }
}
