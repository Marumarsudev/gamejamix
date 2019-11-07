using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public List<GameObject> enemies;
    public List<Transform> spawnpoints;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        InvokeRepeating("SpawnEnemy", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        GameObject tenemy = Instantiate(enemy, spawnpoints[Random.Range(0,spawnpoints.Count)].position, Quaternion.identity);
        tenemy.GetComponent<Enemy>().spawner = this;
        tenemy.GetComponent<Enemy>().goal = player;
        enemies.Add(tenemy);
    }
}
