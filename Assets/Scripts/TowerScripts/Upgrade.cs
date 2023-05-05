using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    [Header("Attack Controller")]
    [SerializeField] AttackController attackController;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI upgradeCostText;
    [SerializeField] GameObject upgradeUI;
    int upgradeCost = 10;


    [Header("Collider")]
    BoxCollider2D boxCollider;

     [Header("Shop")]
    [SerializeField] ShopManager shopManager;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        shopManager = FindObjectOfType<ShopManager>();
        upgradeCostText.text += upgradeCost;
        damageText.text = "Damage: " + attackController.Damage;

    }

    // Update is called once per frame
    void Update()
    {
        MouseButtonDown();
    }

    private void MouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            LayerMask layerMask = LayerMask.GetMask("Towers");

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, .1f, layerMask);

            // Loop through the colliders to check if each one is a BoxCollider2D and perform the desired action for each one that is clicked
            foreach (Collider2D hitCollider in hitColliders)
            {

                BoxCollider2D hitBoxCollider = hitCollider?.GetComponent<BoxCollider2D>();
                
                if (hitBoxCollider != null && hitCollider.gameObject == gameObject)
                {
                    // Do something with the hit collider, which is a BoxCollider2D
                    Debug.Log("Box collider clicked!" + hitCollider.name);
                    upgradeUI.SetActive(!upgradeUI.activeSelf);
                }
            }

        }
        
    }


    private void updateDamage(int damage)
    {
        attackController.Damage= damage;
        damageText.text = "Damage: " + damage;
    }

    public void UpgradeTower()
    {
        if (shopManager.GetMoney() >= upgradeCost)
        {
            updateDamage(  attackController.Damage  + 5);
            shopManager.ChangeMoney(-upgradeCost);
            upgradeCost += 5;
            upgradeCostText.text = "" + upgradeCost;
        }
        else
        {
            Debug.Log("No tenes suficiente platita capo");
        }
    }
}
