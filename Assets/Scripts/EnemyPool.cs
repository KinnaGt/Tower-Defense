using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize;
    public List<GameObject> enemyList;

    void Start()
    {
        enemyList = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyList.Add(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject enemy in enemyList)
        {
            if (!enemy.activeInHierarchy)
            {
                Debug.Log("Activando Enemigo");
                enemy.SetActive(true);
                return enemy;
            }
        }
        Debug.Log("No hay Enemigos Inactivos");
        return null;
    }

    public void DisableAllEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.SetActive(false);
            enemy.transform.position = transform.position;
        }
    }

    public bool hasDisabledChildrens(){
        return enemyList.FindAll(e => e.activeSelf).Count > 0;
    }



}