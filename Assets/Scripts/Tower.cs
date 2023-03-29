using System.Collections;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float baseFiringRate = 2f;
    int damage = 20;
    CircleCollider2D enemyCollider;
    new BoxCollider2D collider;
    private GameObject upgradeUI;
    public bool isFiring = false;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0.3f;
    [SerializeField] float minimumFiringRate = 2f;
    [SerializeField] int enemiesOnRange = 0;

    [Header("Shop")]
    [SerializeField] ShopManager shopManager;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI upgradeCostText;
    int upgradeCost = 10;


    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        enemyCollider = GetComponent<CircleCollider2D>();
        collider = GetComponent<BoxCollider2D>();
        Transform childTransform = transform.Find("UpgradeUI");
        if (childTransform != null)
        {
            upgradeUI = childTransform.gameObject;
        }
        damageText.text = "Damage: " + damage;
        upgradeCostText.text += upgradeCost;
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
            // if (enemiesOnRange == 0)
            // {
            //     isFiring = false; //RESET FIRE
            // }
        }
    }


    void ActiveUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }
    public void DisableUpgradeUI()
    {
        upgradeUI.SetActive(false);
    }


    void OnMouseDown()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hitColliders = Physics2D.OverlapPointAll(mousePosition);

        foreach (Collider2D hitCollider in hitColliders)
        {

            if (hitCollider == collider)
            {
                // Debug.Log("Clicked on the hitbox collider!");
                ActiveUpgradeUI();
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
            instance.GetComponent<DamageDealer>().setDamage(damage);



            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                            baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            yield return new WaitForSeconds(timeToNextProjectile);

        }
        isFiring = false; //RESET FIRE



    }

    private void updateDamage(int damage)
    {
        this.damage = damage;
        damageText.text = "Damage: " + damage;
    }

    public void upgradeTower()
    {
        if (shopManager.GetMoney() >= upgradeCost)
        {
            updateDamage(damage + 5);
            shopManager.ChangeMoney(-upgradeCost);
            upgradeCost += 5;
            upgradeCostText.text = "" + upgradeCost;
        }
        else{
            Debug.Log("No teens suficiente platita capo");
        }
    }

}
