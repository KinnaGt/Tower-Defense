using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemyPool enemyPool;
    public List<Wave> waves;
    [SerializeField] float delaySpawn;
    private int currentWave;

    void Start()
    {
        currentWave = 0;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(delaySpawn);
        while (currentWave < waves.Count)
        {

            Wave wave = waves[currentWave];
            for (int i = 0; i < wave.numEnemies; i++)
            {
                GameObject enemy = enemyPool.GetEnemy();
                yield return new WaitForSeconds(wave.delayBetweenSpawns);
            }
            yield return new WaitForSeconds(wave.delayBetweenWaves);
            currentWave++;
        }
    }
}

[System.Serializable]
public class Wave
{
    public int numEnemies = 5;
    public float delayBetweenSpawns = 1.0f;
    public float delayBetweenWaves = 3.0f;
}
