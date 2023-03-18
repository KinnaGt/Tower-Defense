using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleManager : MonoBehaviour
{
    [SerializeField] private int castleHealth = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReceiveDamage(int value)
    {
        castleHealth -= value;
        if (castleHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
