using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    GameObject closestObject;

    void Start()
    {
        closestObject = null;
    }

    
    
    void Update()
    {
        if (closestObject != null)
        {
            // Calculate the direction towards the closest object
            Vector2 direction = closestObject.transform.position - transform.position;

            // Rotate the projectile towards the closest object
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Move the projectile towards the closest object
            float delta = projectileSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, closestObject.transform.position, delta);


            if (transform.position == closestObject.transform.position)
            {
                Destroy(gameObject);
            }
            if (closestObject.GetComponent<Health>() != null && !closestObject.GetComponent<Health>().isDead())
            {
                Destroy(gameObject);
            }
        }


        closestObject = ClosestObject();
    }

    private GameObject ClosestObject()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        foreach (var obj in FindObjectsOfType<Health>())
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestObject = obj.gameObject;
                closestDistance = distance;
            }
        }

        return closestObject;
    }
    public void setDamage(){
        
    }

}
