using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public Vector2 direction;
    public float speed;

    void Update() 
    { 
        transform.position = new Vector2(transform.position.x + direction.x*speed*Time.deltaTime, transform.position.y + direction.y * speed * Time.deltaTime);
    }

    
}
