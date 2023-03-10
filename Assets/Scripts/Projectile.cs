using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    void Update()
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
        if (closestObject != null)
        {
            float delta = projectileSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, closestObject.transform.position, delta);
            if (transform.position == closestObject.transform.position){
                Destroy(gameObject);
            }
        }
        else{
            Destroy(gameObject);
        }

    }

}
