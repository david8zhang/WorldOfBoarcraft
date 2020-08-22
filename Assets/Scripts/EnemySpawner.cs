using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
    public static int maxEnemies = 3;

    public List<GameObject> enemies;



    // Start is called before the first frame update
    void Start()
    {   
        enemies = new List<GameObject>();
        InvokeRepeating("SpawnEnemy", 0f, 5.0f);
    }

    void SpawnEnemy() {
        int numEnemies = 0;
        foreach (GameObject enemy in enemies) {
            if (enemy) {
                numEnemies++;
            }
        }
        if (numEnemies < maxEnemies) {
            GameObject enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            enemies.Add(enemy);
        }
    }
}
