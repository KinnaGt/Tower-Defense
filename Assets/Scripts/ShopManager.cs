using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] int money = 100;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMoney(int value){
        money += value;
        moneyText.text = "Money: " + money.ToString();
    }

    public int GetMoney(){
        return money;
    }
}
