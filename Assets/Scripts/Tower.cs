using System.Collections;
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
    int enemiesOnRange = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemiesOnRange++;
            Debug.Log("Entra " + enemiesOnRange);
            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine(FireToEnemy());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemiesOnRange--;
            Debug.Log("Sale " + enemiesOnRange);
            if (enemiesOnRange == 0)
            {
                isFiring = false; //RESET FIRE
            }
        }
    }



    IEnumerator FireToEnemy()
    {
        while (enemiesOnRange > 0)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                        transform.position,
                                        Quaternion.identity);


            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                            baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            // Debug.Log("Se espero: " + timeToNextProjectile);
            yield return new WaitForSeconds(timeToNextProjectile);

        }
        isFiring = false; //RESET FIRE



    }

}
