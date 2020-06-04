using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public float range = 2;
    public int enemyCount;
    public int spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GameObject.Find("Sphere").GetComponents(typeof(Letter_TP))[1].getLevel());
        SpawnEnemyWave(1);
    }

    private Vector3 generateSpawnPosition(int height)
    {
        float spawnPosX = Random.Range(-range, range);
        float spawnPosZ = Random.Range(-range, range);
        return new Vector3(spawnPosX, height, spawnPosZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null) { Destroy(this); }
        System.Random randy = new System.Random();
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (0 == randy.Next(enemyCount*1000/spawnRate) && enemyCount < 6) {
            SpawnEnemyWave(randy.Next(4));
            if (0 == randy.Next(3)) { Instantiate(powerupPrefab, generateSpawnPosition(0), enemyPrefab.transform.rotation); }
        }
    }
        
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, generateSpawnPosition(10), enemyPrefab.transform.rotation);
        }
    }

}
