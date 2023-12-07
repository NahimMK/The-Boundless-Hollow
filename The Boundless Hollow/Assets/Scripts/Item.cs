using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public int curLevel;
    public int maxLevel;
    public bool oneTimePickup;
    Player player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncreaseStat()
    {
        curLevel++;
        switch (ID)
        {
            case 0: //Sword
                player.damage += 10;
                break;
            case 1: //Boots
                player.speed += 1;
                break;
            case 2: //Heart
                player.maxHealth += 10;
                break;
            case 3: //Armour
                player.defense += 10;
                break;
            case 4: //Tome
                player.expBonus += 10;
                break;
            case 5://Ring
                player.critPercent += 0.1f;
                break;
            case 6: //Bracelet
                player.critDamage += 0.1f;
                break;
            case 7: //Exp Orb
                player.GainExp();
                break;
            case 8: //Health Potion
                player.Heal(10);
                break;
            default:
                break;
        }
    }
}
