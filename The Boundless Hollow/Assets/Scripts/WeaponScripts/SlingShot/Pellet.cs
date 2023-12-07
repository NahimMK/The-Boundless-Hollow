using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : Projectile
{
    public Vector2 pos;
    public Vector2 dir;
    public float speed;

    void Update()
    {
        transform.position += speed*Time.deltaTime* (Vector3)pos;
    }
}
