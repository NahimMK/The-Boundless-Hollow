using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float health;
    public float baseHealth;
    public float damage;
    public float speed;
    public float defense;
    public int level;
    public GameObject Player;
    private SpriteRenderer sprite;
    private Color originalColor;
    public GameObject damageText;
    public EnemySpawner spawner;


    public GameObject[] DropTable;

    //Debuffs
    public bool onFire; //3 ticks of burn damage
    public bool poisioned; //5 ticks of poision damage
    public bool freezing; //90% move speed debuff
    public bool pulls; //pulls the enemy away from the player

    public int burnTicks;
    public int poisionTicks;
    public float freezeDuration;
    public float pullDuration;

    private float burnTimer;
    private float poisionTimer;
    private float freezeTimer;
    private float pullTimer;

    private float freezeSpeed;
    private float pullSpeed;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
         
    }

    void Awake()
    {
        //Scale player enemy health based off game time and player level
        spawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
        Player = GameObject.Find("Player");
        health = baseHealth + spawner.gameTime * 0.5f * Player.GetComponent<Player>().level;
        freezeSpeed = 0.1f * speed; //90% slow
        pullSpeed = -2;
    }

    void Update()
    {
        if (health <= 0) { Die(); }

        if (pulls)
        {
            Move(pullSpeed);
            pullTimer += Time.deltaTime;
            if(pullTimer > pullDuration) { pulls = false; pullTimer = 0; }
        }
        else if (freezing) { 
            Move(freezeSpeed);
            freezeTimer += Time.deltaTime;
            if (freezeTimer > freezeDuration) { freezing = false; freezeTimer = 0; }
        }
        else { Move(speed); }
        
        //Fire burns 3 times spaced 0.5 seconds apart
        if (onFire)
        {
            burnTimer += Time.deltaTime;
            if(burnTimer >= 0.5f)
            {
                TakeDamage(10, new Color(1.0f, 0.5f, 0.0f));
                burnTimer = 0;
                burnTicks--;
            }
            
            if(burnTicks <= 0)
            { 
                burnTicks = 0;
                onFire = false;
            }
        }


        //Poision ticks 5 times spaced 1 second apart
        if (poisioned)
        {
            poisionTimer += Time.deltaTime;
            if (poisionTimer >= 1)
            {
                TakeDamage(5, Color.magenta);
                poisionTimer = 0;
                poisionTicks--;
            }

            if (poisionTicks <= 0)
            {
                poisionTicks = 0;
                poisioned = false;
            }
        }


    }

    public void TakeDamage(float dmg, Color color) 
    { 
        //Take damage
        health -= dmg; 

        //Create damage text
        GameObject txt = Instantiate(damageText, transform.position + new Vector3(0,1.5f, 0), Quaternion.identity);
        TextMeshPro inside = txt.transform.GetChild(0).GetComponent<TextMeshPro>();
        TextMeshPro outside = txt.transform.GetChild(1).GetComponent<TextMeshPro>();
        inside.text = Mathf.Floor(dmg).ToString();
        outside.text = Mathf.Floor(dmg).ToString();

        //Change color based on debuff or crit
        inside.color = color; inside.outlineColor = color;
        //if (crit) { inside.color = Color.yellow; inside.outlineColor = Color.yellow; }
        //else if (fire) { inside.color = new Color(1.0f, 0.5f, 0.0f); inside.outlineColor = new Color(1.0f, 0.5f, 0.0f); }
        //else if (poison) { inside.color = Color.magenta; inside.outlineColor = Color.magenta; }
        //else { inside.color = Color.red; inside.outlineColor = Color.red; }

        //Destroy damage text
        Destroy(txt, 1f);
    }

    
    
    public void Attack() { }

    public void Ability() { }

    public void Die() { DropItem(); Destroy(this.gameObject); } //spawner.spawnedEnimies.Remove(this.gameObject); }

    //Drops a random item in the enemy's DropTable
    //The drop table also allows for killed enemies to spawn more enimies
    //TODO: Make different items have different drop rates depending on the enemy
    //We'd also probably want to change it so enemies don't drop items 100% of the time
    public void DropItem() { 
        int dropItem = Random.Range(0, DropTable.Length);
        Player.GetComponent<Player>().Enemydefeated++;
        Instantiate(DropTable[dropItem], transform.position, Quaternion.identity);
    }
    public void Move(float moveSpeed)
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime* direction);
    }

    public void TakeKnockback() { }
}

