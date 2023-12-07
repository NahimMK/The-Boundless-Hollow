using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;
    public float damage;
    public float speed;
    public float defense;
    public int level;
    public float levelUp;//Amount of exp needed to level up
    public float exp;
    public float expBonus;
    public float critDamage;
    public float critPercent;
    public int Enemydefeated;

    //Weapons
    public List<int> curWeaponIDs;
    public List<GameObject> curWeapons;
    public List<GameObject> allWeapons;
    public GameObject startingWeapon;

    //Items
    public List<int> curItemIDs;
    public List<GameObject> curItems;
    public List<GameObject> allItems;


    //Items and weapons that can be gained on level up
    public List<int> possibleWeapons;
    public List<int> possibleItems;

    //public List<Item> items; Removeing Item Levels and Item Cap for now

    private SpriteRenderer playerSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        levelUp = 50;//Exp needed for level 1
        level = 1;
        maxHealth = 100;
        curHealth = maxHealth;
        curWeapons = new List<GameObject>();
        curWeaponIDs = new List<int>();
        GameObject starting = Instantiate(startingWeapon);
        starting.transform.SetParent(transform);
        curWeapons.Add(starting);
        curWeaponIDs.Add(starting.GetComponent<Weapon>().weaponID);
        Enemydefeated = 0;

    }



    //TODO: Death and Respawn Mechanics
    public void Die() { Debug.Log("You Died!"); }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.CompareTag("Enemy"))
        {

            float e_damage = collison.GetComponent<Enemy>().damage;
            TakeDamage(e_damage);

        }


    }

    public void TakeDamage(float enemyDamage)
    {
        //Uses armour with diminishing returns
        curHealth -= enemyDamage * (1 - defense / (defense + 200f));
        if(curHealth <= 0) { Die(); }
    }

    public void Heal(float amount)
    {
        curHealth = Mathf.Min(curHealth + amount, maxHealth);
    }

    //Uses a GameObject and Item ID system
    //Every Unique Item GameObject has a unique ID attached with different functionality
    //Thre are no item level caps and no limit on the amount of items that can be held currently
    //Any special abilities an item might have can also be added here
    public void ItemPickup(int ID)
    {
        switch (ID)
        {
            case 0: //Sword
                damage += 10;
                break;
            case 1: //Boots
                speed += 1;
                break;
            case 2: //Heart
                maxHealth += 10;
                break;
            case 3: //Armour
                defense += 10;
                break;
            case 4: //Tome
                expBonus += 10;
                break;
            case 5://Ring
                critPercent += 0.1f;
                break;
            case 6: //Bracelet
                critDamage += 0.1f;
                break;
            case 7: //Exp Orb
                GainExp();
                break;
            case 8: //Health Potion
                Heal(10);
                break;
            default:
                break;
        }
    }

    public void WeaponPickup(int ID)
    {
        //If the weapon that is picked up is not currently held, then create a new weapon
        //If it is already in inventory, level up the weapon
        bool hasWeapon = false;
        for(int i = 0; i < curWeapons.Count; i++)
        {
            Weapon weapon = curWeapons[i].GetComponent<Weapon>();
            if (weapon.weaponID == ID){

                //If weapon can evolve, then evolve
                if (weapon.level == weapon.maxLevel && weapon.evolveID != -1)
                {
                    GameObject evolvedWeapon = Instantiate(allWeapons[weapon.evolveID]);
                    curWeapons.Add(evolvedWeapon);
                    evolvedWeapon.transform.SetParent(transform);
                    curWeaponIDs.Add(weapon.evolveID);
                    curWeaponIDs.Remove(weapon.weaponID);
                    Destroy(curWeapons[i]);
                    curWeapons.Remove(curWeapons[i]); 
                }
                //Weapon cannot evolve, level up the weapon
                else
                {
                    weapon.LevelUp(curWeapons[i].GetComponent<LevelManager>().GetLevelUpStats());
                    curWeapons[i].GetComponent<LevelManager>().LevelUpdate();
                }
                hasWeapon = true;
            }
        }

        //Add new weapon to inventory if evolved version is not currently held
        if (!hasWeapon && !curWeaponIDs.Contains(allWeapons[ID].GetComponent<Weapon>().evolveID)) 
        {
            GameObject weapon = Instantiate(allWeapons[ID]);
            curWeapons.Add(weapon);
            weapon.transform.SetParent(transform);
            curWeaponIDs.Add(ID);
        }

    }

    public void GainExp()
    {
        //Exp gain per pick up
        float xp = Random.Range(1, 20);

        //Exp gain scales with expBonus
        exp += xp + xp * (expBonus / 100);

        //Leveling Up
        if (exp >= levelUp)
        {
            level++;
            exp = Mathf.Max(levelUp - exp, 0); //Carryover exp

            //Next Level scales
            //TODO: Figure out what formula and what numbers feel best
            levelUp = Mathf.RoundToInt(50 * Mathf.Pow(level, 0.5f));//Exponential Formula
            //levelUp = 50 + (level - 1) * 0.5f;//Polynomial Formula

            //TODO: Give stats or items or something to the player for leveling up

            //Stores Item/Weapon ID and true false for if it is an item or a weapon
            List<(int, bool)> choices = new List<(int, bool)>();

            for(int i = 0; i < 3; i++)
            {
                //Add Weapon to list
                if (Random.Range(0f, 1f) > 0.5f){ choices.Add((possibleWeapons[Random.Range(0, possibleWeapons.Count)], false)); }
                //Add Item to list
                else { choices.Add((possibleItems[Random.Range(0, possibleItems.Count)], true)); }
            }

            //Pick one of the 3 choices and give it to the player
            //Need to change it so player can choose which upgrade they want
            (int, bool) choice = choices[Random.Range(0, 3)];
            if (choice.Item2 == false){WeaponPickup(choice.Item1);}
            else { ItemPickup(choice.Item1);}

            for(int j = 0; j < 3; j++) { Debug.Log(choices[j].Item1 + " " + choices[j].Item2); }
            Debug.Log("Item ID: " + choice.Item1 + " Is Item: " + choice.Item2);
        }
    }

}
