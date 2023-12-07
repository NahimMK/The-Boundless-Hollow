using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy 
{
    public void Move()
    {
        Vector3 direction = (Player.gameObject.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
