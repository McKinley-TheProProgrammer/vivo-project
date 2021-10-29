using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public int TakeDamage(int dmgAmount)
    {
        return dmgAmount;
    }

    public void TakeDamage(int takesDamage, int dmgAmount)
    {
        takesDamage -= dmgAmount;
    }
}
