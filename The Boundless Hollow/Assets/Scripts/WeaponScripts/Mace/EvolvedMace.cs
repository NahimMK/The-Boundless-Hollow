using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvolvedMace : Projectile
{

    public int hits;
    public GameObject ElectricField;

    // Start is called before the first frame update
    void Awake()
    {
        hits = 0;
        player =  GameObject.Find("Player").GetComponent<Player>();
    }

    //Damage Enemies that are hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            hits++;
            float rand = Random.value;
            float dmgMultiplier = player.damage;
            bool crit = false;
            if (rand <= player.critPercent) { damage += (1 + player.critDamage) * damage; crit = true; }
            
            if (crit) { collision.GetComponent<Enemy>().TakeDamage(damage * dmgMultiplier, Color.white); }
            else { collision.GetComponent<Enemy>().TakeDamage(damage * dmgMultiplier, Color.red); }
            collision.GetComponent<Enemy>().TakeKnockback();
  

            if (hits == 5)
            {
                hits = 0;
                GameObject field = Instantiate(ElectricField, transform.position, Quaternion.identity);
                Destroy(field, 3f);
            }

            if (!peircing) { Destroy(gameObject); }
        }
    }

}
