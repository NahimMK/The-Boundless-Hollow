using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolvedClaymoreProjectile : Projectile
{
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        ability = new EvolvedClaymoreHeal(player);
    }
}

public class EvolvedClaymoreHeal : SpecialAbilities
{
    Player player;
    public EvolvedClaymoreHeal(Player play) { player=play; }

    //10% lifesteal on hit
    public void SpecialAbility() { player.Heal(player.damage * 0.1f); }
}