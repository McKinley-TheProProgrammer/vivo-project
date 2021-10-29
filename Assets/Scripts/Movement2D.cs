using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour
{

    [SerializeField] private float speed = 6.5f;

    private Rigidbody2D myBody;
    
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    public Vector2 Move(float x, float y, bool normalize)
    {
        Vector2 movement = new Vector2(x, y);

        myBody.velocity = normalize ? movement.normalized * speed : movement * speed;

        return myBody.velocity * 10 * Time.fixedDeltaTime;
    }
    
    
    public Vector2 MoveTo(Vector2 a, Vector2 b, float t)
    {
        Vector2 posToGoTo = Vector2.Lerp(a, b, t);

        return posToGoTo;
    }
}
