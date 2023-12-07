using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claymore : Weapon, LevelManager
{
    private Vector2 lastInput = new Vector2(1, 0);
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
            
            float angle = Mathf.Atan2(lastInput.y, lastInput.x);
            float deg = angle * Mathf.Rad2Deg;
            deg = (deg + 360) % 360;
            GameObject attack = Instantiate(projectile, new Vector2(pos.x + lastInput.x * 2, pos.y + lastInput.y * 2), Quaternion.Euler(0, 0, deg));
            Destroy(attack, 1f);
            attack.transform.localScale = new Vector3(size, size, 0);
            StartCoroutine(FadeOut(attack));
            yield return new WaitForSeconds(multiProjectileTimeDelay);
            
        }

    }

    IEnumerator FadeOut(GameObject obj)
    {
        SpriteRenderer objSprite = obj.GetComponent<SpriteRenderer>();
        Color initial = objSprite.color;
        float fadeSpeed = 1f / projectileDuration;

        for (float t = 0f; t < 1f; t += Time.deltaTime * fadeSpeed)
        {
            Color newColor = new Color(initial.r, initial.g, initial.b, Mathf.Lerp(1f, 0f, t));
            objSprite.color = newColor;

            yield return null; // Wait for the next frame
        }

        // Ensure that the alpha value is exactly 0 at the end
        objSprite.color = new Color(initial.r, initial.g, initial.b, 0f);
    }

    public List<float> GetLevelUpStats()
    {
        List<float> stats;

        //Gets the stats that increase with the level up
        //Order: Cooldown, Size, ProjectileSpeed, ProjectileCount, ProjectileDuration
        switch (level)
        {
            case 1:
                stats = new List<float> { 0.1f, 0.1f, 0, 1, 0, 0 };
                break;
            case 2:
                stats = new List<float> { 0.1f, 0.1f, 0, 0, 0, 0 };
                break;
            case 3:
                stats = new List<float> { 0.1f, 0.1f, 0, 1, 0, 0 };
                break;
            case 4:
                stats = new List<float> { 0.1f, 0.1f, 0, 0, 0, 0 };
                break;
            case 5:
                stats = new List<float> { 0.1f, 0.1f, 0, 1, 0, 0 };
                break;
            case 6:
                stats = new List<float> { 0.1f, 0.1f, 0, 0, 0, 0 };
                break;
            case 7:
                stats = new List<float> { 0.2f, 0.2f, 0, 0, 0, 0 };
                break;
            case 8:
                stats = new List<float> { 0.2f, 0.2f, 0, 1, 0, 0 };
                break;
            default:
                stats = new List<float> { 0,0,0,0,0 };
                break;
        }

        return stats;
    }

    public void LevelUpdate() { }

}
