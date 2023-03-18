using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    [SerializeField] ParticleSystem hitEffect;
    CastleManager castleManager;


    void Start()
    {
        castleManager = FindObjectOfType<CastleManager>();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }



    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {

        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            castleManager.ReceiveDamage(20);
            Reset();
        }
    }

    public void Reset()
    {
        waypointIndex = 0;
        transform.position = waypoints[0].position;
        gameObject.SetActive(false);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }

    }
}

