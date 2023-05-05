using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{


    CircleCollider2D enemyCollider;


    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float baseFiringRate = 2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0.3f;
    [SerializeField] float minimumFiringRate = 2f;
    [SerializeField] int enemiesOnRange = 0;
    [SerializeField] int damage = 20;
    public bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<CircleCollider2D>();
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemiesOnRange++;
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
        }
    }



    IEnumerator FireToEnemy()
    {
        while (enemiesOnRange > 0 && useAI)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                        transform.position,
                                        Quaternion.identity);
            instance.GetComponent<DamageDealer>().setDamage(damage);



            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                            baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            yield return new WaitForSeconds(timeToNextProjectile);

        }
        isFiring = false; //RESET FIRE



    }




    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}
