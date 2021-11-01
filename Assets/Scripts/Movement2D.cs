using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour
{

    [SerializeField] private float speed = 6.5f;

    private Rigidbody2D myBody;

    public Rigidbody2D GetRigidBody2D() => myBody;
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
    
    
    public Vector2 Move(Vector2 dir, bool normalize)
    {
        myBody.velocity = normalize ? dir.normalized * speed : dir * speed;

        return myBody.velocity * 10 * Time.fixedDeltaTime;
    }
    public void MoveTo(Vector2 a, float dur)
    {
        //Vector2 posToGoTo = Vector2.Lerp(a, b, t);

        Tween moveToTween = myBody.transform.DOBlendableMoveBy(a, dur);
        
    }
}
