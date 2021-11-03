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
            myHp = hpAux;
            switch (this.gameObject.tag)
            {
                case "Enemy":
                    ScoreSystem.Instance.MultiplyScore((int)playerScore);
                    SpriteRenderer enemySprite = GetComponent<SpriteRenderer>();
                    enemySprite.color = Color.white;

                    GameObject enemyVFX = Pooling.Instance.SpawnFromPool("EnemyDeathVFX", transform.position, transform.rotation);
                    break;
                case "Player":
                    
                    CacheDeath();
                    StartCoroutine(PlayerDies());
                    break;
            }
            gameObject.SetActive(false);
            
        }
        return hpLoss;
    }

    void CacheDeath()
    {
        GetComponent<Movement2D>().GetRigidBody2D().gravityScale = 2f;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }
    IEnumerator PlayerDies()
    {
        CacheDeath();
        yield return new WaitForSeconds(1f);
        StartCoroutine(GameManager.EndGame(1f));
        StartCoroutine(ScoreSystem.Instance.Score(Damage.playerScore));
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
