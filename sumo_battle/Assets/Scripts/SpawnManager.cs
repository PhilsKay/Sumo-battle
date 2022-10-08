using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9;
    private int enmiesToSpawn = 1;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enmiesToSpawn);
        Instantiate(powerupPrefab, GenerateSpwanPosition(), powerupPrefab.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if(enemyCount == 0)
        {
            enmiesToSpawn++;
            Instantiate(powerupPrefab, GenerateSpwanPosition(), powerupPrefab.transform.rotation);
            SpawnEnemyWave(enmiesToSpawn);
        }
    }
    private Vector3 GenerateSpwanPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        return spawnPos;
    }
    void SpawnEnemyWave(int enemies)
    {
        for (int i = 1; i <= enemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpwanPosition(), enemyPrefab.transform.rotation);
        }
    }
}
