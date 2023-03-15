using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 10;
    int defaultHealth;
    [SerializeField] ParticleSystem hitEffect;
    ShopManager shopManager;

    // Start is called before the first frame update
    void Start()
    {
        defaultHealth = health;
        shopManager = FindObjectOfType<ShopManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
        }
    }



    void TakeDamage(int damage)
    {
        health -= damage;
        PlayHitEffect();
        if (health <= 0)
        {
            shopManager.ChangeMoney(25);
            health = defaultHealth;
            GetComponent<EnemyPathfinder>().Reset();
        }
    }

    public bool isDead(){
        return gameObject.activeSelf;
    }
    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }

    }
}
