using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthComponent; // reference to the enemy's health component
    private Image fillAmountImage;

    void Start()
    {
        fillAmountImage = GetComponent<Image>();
    }
    private void Update()
    {

        if (healthComponent != null)
        {
            // Update the health bar value based on the enemy's health
            float healthValue = healthComponent.GetCurrentHealth();
            float maxHealthValue = healthComponent.GetMaxHealth();


            // Position the health bar slider above the enemy
            fillAmountImage.fillAmount = healthValue / maxHealthValue;

        }
        else
        {
            // Destroy the health bar if the enemy is dead
            Destroy(gameObject);
            
        }
    }
}
