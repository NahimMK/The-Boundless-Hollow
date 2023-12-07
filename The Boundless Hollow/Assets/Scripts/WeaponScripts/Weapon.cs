using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponID;
    public int evolveID;
    public GameObject projectile;
    public float damage;
    public float cooldown;
    public float size;
    public float projectileSpeed;
    public int projectileCount;
    public float projectileDuration;
    public int level;
    public int maxLevel;
    public float cooldownTimer = 0f;
    public float multiProjectileTimeDelay;
    public GameObject player;
    public EnemySpawner spawner;

    void Start()
    {
        player = GameObject.Find("Player");
        spawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
    }

    public void LevelUp(List<float> stats)
    {
        level = Mathf.Min(level+1, maxLevel);
        cooldown -= stats[0];
        size += stats[1];
        projectileSpeed += stats[2];
        projectileCount += ((int)stats[3]);
        projectileDuration += stats[4];
        damage += stats[5];
    }
    
}

public interface LevelManager
{
    public List<float> GetLevelUpStats();
    public void LevelUpdate();
}
