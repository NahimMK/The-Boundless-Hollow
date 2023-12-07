using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class StormScissors : Weapon

{
    public GameObject projectile2;
    public GameObject projectile3;
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
        Vector2 pos1 = player.transform.position;
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 pos = player.transform.position;

            float angle = Mathf.Atan2(lastInput.y, lastInput.x);
            float deg = angle * Mathf.Rad2Deg;
            deg = (deg + 360) % 360;
            if (i % 3 == 0)
            {
                GameObject attack3 = Instantiate(projectile3, new Vector2(pos.x + lastInput.x * 2, pos.y + lastInput.y * 2), Quaternion.Euler(0, 0, deg));
                attack3.gameObject.GetComponent<Arrow>().direction = lastInput;
                attack3.gameObject.GetComponent<Arrow>().speed = projectileSpeed;
                attack3.GetComponentInChildren<Projectile>().damage = damage;
                attack3.transform.localScale = new Vector3(size, size, 0);
                Destroy(attack3, 6);
            }
            if (i % 2 == 0)
            {
                GameObject attack = Instantiate(projectile, new Vector2(pos.x + lastInput.x * 2, pos.y + lastInput.y * 2), Quaternion.Euler(0, 0, deg));
                attack.transform.localScale = new Vector3(size, size, 0);
                attack.GetComponentInChildren<Projectile>().damage = damage;
                StartCoroutine(FadeOut(attack));
            }
            
            else
            {
                GameObject attack2 = Instantiate(projectile2, new Vector2(pos.x + lastInput.x * 2, pos.y + lastInput.y * 2), Quaternion.Euler(0, 0, deg));
                attack2.transform.localScale = new Vector3(size, size, 0);
                attack2.GetComponentInChildren<Projectile>().damage = damage;
                StartCoroutine(FadeOut(attack2));
            }
            yield return new WaitForSeconds(multiProjectileTimeDelay);
        }
        

    }
    IEnumerator FadeOut(GameObject obj)
    {
        if (obj != null)
        {
            SpriteRenderer objSprite = obj.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Color initial = objSprite.color;
            float fadeSpeed = 1f / projectileDuration;

            for (float t = 0f; t < 1f; t += Time.deltaTime * fadeSpeed)
            {
                Color newColor = new Color(initial.r, initial.g, initial.b, Mathf.Lerp(1f, 0f, t));
                //objSprite.color = newColor;

                yield return null; // Wait for the next frame
            }

            // Ensure that the alpha value is exactly 0 at the end
            //objSprite.color = new Color(initial.r, initial.g, initial.b, 0f);
            if (obj != null)
                Destroy(obj);
        }

    }

}
