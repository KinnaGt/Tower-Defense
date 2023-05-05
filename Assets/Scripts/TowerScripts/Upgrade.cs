using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
           
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D boxHit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, LayerMask.GetMask("Towers"));
            if (boxHit.collider != null && boxHit.collider == boxCollider)
            {
                upgradeUI.SetActive(!upgradeUI.activeSelf);
            }
        }


    }


    private void updateDamage(int damage)
    {
        attackController.Damage = damage;
        damageText.text = "Damage: " + damage;
    }

    public void UpgradeTower()
    {
        if (shopManager.GetMoney() >= upgradeCost)
        {
            updateDamage(attackController.Damage + 5);
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
