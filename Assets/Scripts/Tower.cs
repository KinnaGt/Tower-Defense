using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float baseFiringRate = 2f;

    public bool isFiring = false;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0.3f;
    [SerializeField] float minimumFiringRate = 2f;
    List<Collider2D> enemiesOnRange;

    void Start()
    {
        enemiesOnRange = new List<Collider2D>();
    }





    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemiesOnRange.Add(other);
            Debug.Log(enemiesOnRange.Count);
            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine(FireToEnemy());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemiesOnRange.Remove(other);
    }



    IEnumerator FireToEnemy()
    {
        while (enemiesOnRange.Count > 0)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                        transform.position,
                                        Quaternion.identity);


            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                            baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            Debug.Log("Se espero: " + timeToNextProjectile);
            yield return new WaitForSeconds(timeToNextProjectile);
            isFiring = false;

        }


    }

}
