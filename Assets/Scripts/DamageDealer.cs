using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 0;

    public int GetDamage()
    {
        return damage;
    }

    
}
