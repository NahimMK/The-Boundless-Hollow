using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : Weapon, LevelManager
{
    private Vector2 lastInput = new Vector2(1, 0);
    private float range = 15f;
    

    void Update()
    {

        cooldownTimer += Time.deltaTime;
        if (player.GetComponent<Movement>().input != new Vector2(0, 0)) { lastInput = player.GetComponent<Movement>().input; }

        if (cooldownTimer > cooldown)
        {
            cooldownTimer = 0f;
            StartCoroutine(Attack());
        }

    }
    IEnumerator Attack()
    {

        GameObject target = GetClosestEnemy();
        float dist = Vector3.Distance(player.transform.position, target.transform.position);
        if(target != null && dist <= range )
        {
            Vector2 dir = target.transform.position;
            Vector2 pos = player.transform.position;
            GameObject attack = Instantiate(projectile, pos, Quaternion.identity);
            attack.gameObject.GetComponent<Pellet>().pos = (dir - pos).normalized;
            attack.gameObject.GetComponent<Pellet>().speed = projectileSpeed;

            Destroy(attack, projectileDuration);
        }

        yield break;
    }

    GameObject GetClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject enemy in spawner.spawnedEnimies)
        {
            if(enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
    


    public List<float> GetLevelUpStats()
    {
        List<float> stats;

        //Gets the stats that increase with the level up
        //Order: Cooldown, Size, ProjectileSpeed, ProjectileCount, ProjectileDuration, damage
        switch (level)
        {
            case 1:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 0 };
                break;
            case 2:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 0 };
                break;
            case 3:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 0 };
                break;
            case 4:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 0 };
                break;
            case 5:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 7 };
                break;
            case 6:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 0 };
                break;
            case 7:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 0 };
                break;
            case 8:
                stats = new List<float> { 0.225f, 0, 0, 0, 0, 7 };
                break;
            default:
                stats = new List<float> { 0, 0, 0, 0, 0, 0 };
                break;
        }

        return stats;
    }

    public void LevelUpdate() { }

}
