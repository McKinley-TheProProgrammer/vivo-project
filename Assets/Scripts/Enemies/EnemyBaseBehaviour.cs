using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class EnemyBaseBehaviour : MonoBehaviour
{
    public float dmgAmount = 10;
    protected Damage enemyDamage;
    public abstract void DoAttack();
    
    protected SpriteRenderer spriteRenderer;
    
   
    public IEnumerator FlashHit(SpriteRenderer otherMat,float rate)
    {
        Tween changeColor = otherMat.DOBlendableColor(Color.red, rate).SetAutoKill(false);
        yield return new WaitForSeconds(rate);
        changeColor.PlayBackwards();
        yield return new WaitForSeconds(rate);
        if (changeColor.IsComplete())
        {
            changeColor.Kill();
            print("I'm in");
                
        }

    }
}
