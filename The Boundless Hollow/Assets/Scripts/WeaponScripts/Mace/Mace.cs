using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : Weapon, LevelManager
{
    private float radius = 5f;
    private List<GameObject> mace;
    public List<TrailRenderer> trailRenderers;
    // Update is called once per frame
    private void Awake()
    {
        mace = new List<GameObject>();
        player = GameObject.Find("Player");

        //Create maces equally spaced out from one another
        for (int i = 0; i < projectileCount; i++)
        {
            float angle =  (360f / projectileCount * i) * Mathf.Deg2Rad;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            mace.Add(Instantiate(projectile, new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity));

            trailRenderers.Add(mace[i].gameObject.AddComponent<TrailRenderer>());
            InitializeTrailRenderer(trailRenderers[i]);
        }
        
    }
    void Update()
    {
       
        for(int i = 0; i < mace.Count; i++)
        {
            float angle = Time.time * projectileSpeed + ((360f / mace.Count) * i) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Set the position of the object relative to the player
            mace[i].transform.position = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
        }
        

    }
    void InitializeTrailRenderer(TrailRenderer trail)
    {
        // Set up Trail Renderer properties
        float t = Mathf.InverseLerp(3f, 5f, projectileSpeed);
        trail.time = Mathf.Lerp(0.5f, 0.3f, t); 
        trail.startWidth = 0.75f;
        trail.endWidth = 0.0f; // Trail tapers off
        trail.sortingLayerName = "Player";
        trail.sortingOrder = 0;
        // Optionally, you can customize other properties of the Trail Renderer
        // For example, you can adjust the material, color, etc.
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = Color.white;
        trail.endColor = Color.clear;
    }

    public List<float> GetLevelUpStats()
    {
        List<float> stats;

        //Gets the stats that increase with the level up
        //Order: Cooldown, Size, ProjectileSpeed, ProjectileCount, ProjectileDuration, Damage
        switch (level)
        {
            case 1:
                stats = new List<float> { 0, 0, 0.25f, 0, 0, 0 };
                break;
            case 2:
                stats = new List<float> { 0, 0, 0.25f, 1, 0, 0 };
                break;
            case 3:
                stats = new List<float> { 0, 0.25f, 0.25f, 0, 0, 0 };
                break;
            case 4:
                stats = new List<float> { 0, 0, 0.25f, 1, 0, 0 };
                break;
            case 5:
                stats = new List<float> { 0, 0, 0.25f, 0, 5 };
                break;
            case 6:
                stats = new List<float> { 0, 0, 0.25f, 1, 0, 0 };
                break;
            case 7:
                stats = new List<float> { 0, 0.25f, 0.25f, 0, 0, 0 };
                break;
            case 8:
                stats = new List<float> { 0, 0, 0.25f, 1, 0, 5 };
                break;
            default:
                stats = new List<float> { 0, 0, 0, 0, 0 };
                break;
        }

        return stats;
    }

    public void LevelUpdate()
    {
        //Update number of projectiles
        if(mace.Count != projectileCount)
        {
            mace.Add(Instantiate(projectile));
            trailRenderers.Add(mace[mace.Count-1].gameObject.AddComponent<TrailRenderer>());
            InitializeTrailRenderer(trailRenderers[mace.Count - 1]);
            for (int i = 0; i < projectileCount; i++)
            {
                float angle = (360f / projectileCount * i) * Mathf.Deg2Rad;

                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                mace[i].transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + y, 0);

            }
        }

        //Update projectile size
        for(int i= 0; i < projectileCount; i++)
        { 
            trailRenderers[i].startWidth = size;
        }
    }
}
