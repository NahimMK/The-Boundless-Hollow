using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KabutoPellet : Pellet
{
    private int augment;
    private int maxBounces = 3;
    private int bounces = 0;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        ability = null;
        augment = Random.Range(0, 6);
    }

    //Damage Enemies that are hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float rand = Random.value;
            float damageMultiplier = player.damage;
            bool crit = false;
            if (rand <= player.critPercent) { damage += (1 + player.critDamage) * damage; crit = true; }

            if (crit) { collision.GetComponent<Enemy>().TakeDamage(damage * damageMultiplier, Color.white); }
            else { collision.GetComponent<Enemy>().TakeDamage(damage * damageMultiplier, Color.red); }
            collision.GetComponent<Enemy>().TakeKnockback();

            switch (augment)
            {
                case 0: //Fire pellet
                    collision.GetComponent<Enemy>().onFire = true;
                    collision.GetComponent<Enemy>().burnTicks = 3;
                    break;
                case 1: //Poision Pellet
                    collision.GetComponent<Enemy>().poisioned = true;
                    collision.GetComponent<Enemy>().poisionTicks = 5;
                    break;
                case 2: //Peircing Pellet
                    peircing = true;
                    break;
                case 3: //Bouncing Pellet
                    peircing = true;
                    pos = Vector2.Reflect(pos, collision.transform.up);
                    bounces++;
                    break;
                case 4: //Freezing Pellet
                    collision.GetComponent<Enemy>().freezing = true;
                    collision.GetComponent<Enemy>().freezeDuration = 10;
                    break;
                default://Normal Bullet
                    break;
            }



            if (!peircing || bounces >= maxBounces) { Destroy(gameObject); }


        }
    }

}
