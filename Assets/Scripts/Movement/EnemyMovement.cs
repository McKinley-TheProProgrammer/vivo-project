using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum MovementTypes
{
    MOVE,
    SINE_WAVE
}

[RequireComponent(typeof(Movement2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private MovementTypes m_MovementTypes;
    
    private Movement2D enemyMovement;

    public float steerAmp = .31f;
    private void Start()
    {
        enemyMovement = GetComponent<Movement2D>();
    }

    public void Sine_Movement()
    {
        enemyMovement.Move(new Vector2(Mathf.Sin(Time.time + Random.Range(0f, steerAmp)), -1), false);
    }

    void Normal_Movement()
    {
        enemyMovement.Move(Vector2.down, true);
    }
    
    void FixedUpdate()
    {
        switch (m_MovementTypes)
        {
            case MovementTypes.MOVE:
                Normal_Movement();
                break;
            case MovementTypes.SINE_WAVE:
                Sine_Movement();
                break;
        }
            
    }
}
