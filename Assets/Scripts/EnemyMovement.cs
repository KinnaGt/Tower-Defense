using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float moveSpeed = 1f;

    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity += new Vector2(moveSpeed * Time.deltaTime,0);
    }
}
