using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBat : MonoBehaviour
{
    [SerializeField] private float amp = .56f, frequency = 2;
    private Movement2D movement2D;
    
    void Start()
    {
        movement2D = GetComponent<Movement2D>();
    }

    void MovingSideways()
    {
        movement2D.Move(new Vector2(1f, Mathf.Cos(Time.time + frequency) * amp), true);
    }

    
    void FixedUpdate()
    {
        MovingSideways();
        
    }
}
