using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate;
    public GameObject player;
    public List<GameObject> enemies;
    private float spawnTimer = 0f;
    public List<GameObject> spawnedEnimies;
    public float gameTime;
    public int BossesSpawned;

    void Update()
    {
        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        //if the spawn timer has surpassed the spawn rate make a new enemy
        if (spawnTimer > spawnRate)
        {
            spawnTimer = 0f;
            int enemyNum = 0;
            //Stops enemies from spawning on top of the player, and spawns them a random distance away from the player
            float[] xpos = { Random.Range(-10f, -5f), Random.Range(5f, 10f) };
            float[] ypos = { Random.Range(-10f, -5f), Random.Range(5f, 10f) };
            //get the vector for the enemy spawn
            Vector2 spawnPos = new Vector2(player.transform.position.x + xpos[Random.Range(0, 2)], player.transform.position.y + ypos[Random.Range(0, 2)]);
            //spawn the enemy
            spawnedEnimies.Add(Instantiate(enemies[enemyNum], spawnPos, Quaternion.identity));
        }

        //for every 100 enimies defeated spawn the boss
        if (player.GetComponent<Player>().Enemydefeated > 100 * (BossesSpawned + 1))
        {
            BossesSpawned++;
            int enemyNum = 1;
            //Stops enemies from spawning on top of the player
            float[] xpos = { Random.Range(-10f, -5f), Random.Range(5f, 10f) };
            float[] ypos = { Random.Range(-10f, -5f), Random.Range(5f, 10f) };
            //get the vector for the boss spawn
            Vector2 spawnPos = new Vector2(player.transform.position.x + xpos[Random.Range(0, 2)], player.transform.position.y + ypos[Random.Range(0, 2)]);
            //spawn the boss
            spawnedEnimies.Add(Instantiate(enemies[enemyNum], spawnPos, Quaternion.identity));

        }
    }
}
