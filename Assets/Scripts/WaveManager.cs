using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemyPool enemyPool;
    public List<Wave> waves;
    [SerializeField] float delaySpawn;
    private int totalWaves;
    private int remainingWaves;
    private int currentWave;

    void Start()
    {
        currentWave = 0;
        StartCoroutine(SpawnWaves());
        totalWaves = waves.Count;
        remainingWaves = totalWaves;
    }

    public int GetTotalWaves()
    {
        return totalWaves;
    }

    public int GetRemainingWaves()
    {
        return remainingWaves;
    }



    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(delaySpawn);
        while (currentWave < waves.Count)
        {

            Wave wave = waves[currentWave];
            for (int i = 0; i < wave.GetNumEnemies(); i++)
            {
                GameObject enemy = enemyPool.GetEnemy();
                yield return new WaitForSeconds(wave.delayBetweenSpawns);
            }
            remainingWaves--;
            yield return new WaitForSeconds(wave.delayBetweenWaves);
            currentWave++;
        }
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] int numEnemies = 5;
    public float delayBetweenSpawns = 1.0f;
    public float delayBetweenWaves = 3.0f;

    public int GetNumEnemies()
    {
        return numEnemies;
    }
}
