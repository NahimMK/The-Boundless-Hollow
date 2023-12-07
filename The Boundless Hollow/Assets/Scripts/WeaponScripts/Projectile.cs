using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Player player;
    public SpecialAbilities ability;
    public bool peircing;
    public bool setsOnFire;
    public bool poisons;
    public bool electrocutes;
    public bool pulls;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        ability = null;
    }

    //Damage Enemies that are hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float rand = Random.value;
            float dmg = player.damage;
            bool crit = false;
            if (rand <= player.critPercent) { dmg += (1+player.critDamage) * dmg; crit = true; }

            if (crit) { collision.GetComponent<Enemy>().TakeDamage(dmg, Color.white); }
            else { collision.GetComponent<Enemy>().TakeDamage(dmg, Color.red); }
            collision.GetComponent<Enemy>().TakeKnockback();

            if (setsOnFire) 
            {
                collision.GetComponent<Enemy>().onFire = true;
                collision.GetComponent<Enemy>().burnTicks = 3;
            }

            if (poisons)
            {
                collision.GetComponent<Enemy>().poisioned = true;
                collision.GetComponent<Enemy>().poisionTicks = 5;
            }

            if (pulls)
            {
                collision.GetComponent<Enemy>().pulls = true;
                collision.GetComponent<Enemy>().pullDuration = 2.5f;
            }

            if (ability != null)
            {
                ability.SpecialAbility();
            }

            if (!peircing) { Destroy(gameObject); }
        }
    }

}

//Allows override for anything special a weapon does.
//Set enemy on fire, give lifesteal, etc.
public interface SpecialAbilities
{
    void SpecialAbility();
}