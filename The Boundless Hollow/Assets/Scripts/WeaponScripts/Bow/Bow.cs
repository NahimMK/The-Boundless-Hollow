using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon, LevelManager
{
    private Vector2 lastInput = new Vector2(1, 0);
    public bool poision;
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

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 pos = player.transform.position;
            float yoffset = Random.Range(-1f, 1f);
            float xoffset = Random.Range(-1f, 1f);

            float angle = Mathf.Atan2(lastInput.y, lastInput.x);
            float deg = angle * Mathf.Rad2Deg;
            deg = (deg + 360) % 360;

            GameObject attack = Instantiate(projectile, new Vector2(pos.x + xoffset + lastInput.x * 2, pos.y + yoffset+ lastInput.y * 2), Quaternion.Euler(0, 0, deg-90));
            attack.gameObject.GetComponent<Arrow>().direction = lastInput;
            attack.gameObject.GetComponent<Arrow>().speed = projectileSpeed;
            if (poision) { attack.gameObject.GetComponent<Arrow>().poisons = true; attack.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta; }

            Destroy(attack, projectileDuration);
            yield return new WaitForSeconds(multiProjectileTimeDelay);
        }

    }

    public List<float> GetLevelUpStats()
    {
        List<float> stats = new List<float> { 0, 0, 0, 0, 0 };

        //If weapon is already evolved do nothing
        if(weaponID == 4) { return stats; }

        //Gets the stats that increase with the level up
        //Order: Cooldown, Size, ProjectileSpeed, ProjectileCount, ProjectileDuration
        switch (level)
        {
            case 1:
                stats = new List<float> { 0, 0, 0, 1, 0, 0 };
                break;
            case 2:
                stats = new List<float> { 0, 0, 0, 1, 0, 0 };
                break;
            case 3:
                stats = new List<float> { 0, 0, 0, 1, 0, 0 };
                break;
            case 4:
                stats = new List<float> { 0, 0, 0, 1, 0, 0 };
                break;
            case 5:
                stats = new List<float> { 0.5f, 0, 0, 0, 0, 5 };
                break;
            case 6:
                stats = new List<float> { 0, 0, 0, 1, 0, 0 };
                break;
            case 7:
                stats = new List<float> { 0, 0, 0, 1, 0, 0 };
                break;
            case 8:
                stats = new List<float> { 0.5f, 0, 0, 1, 0, 0 };
                break;
            default:
                stats = new List<float> { 0, 0, 0, 0, 0, 5 };
                break;
        }

        return stats;
    }
    
    public void LevelUpdate() { }

}

