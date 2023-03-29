using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private int damage = 0;

    public int GetDamage()
    {
        return damage;
    }

    public void setDamage(int damage){
        this.damage = damage;
    }

    
}
