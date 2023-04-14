using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointFollow : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float waitTime = 1f;

    private int currentWaypointIndex = 0;
    private float timeSinceLastWaypoint = 0f;

    private void Update()
    {
        Transform currentWaypoint = waypoints[currentWaypointIndex];

        
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);

        
        if (transform.position == currentWaypoint.position)
        {
            timeSinceLastWaypoint += Time.deltaTime;
            if (timeSinceLastWaypoint >= waitTime)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                timeSinceLastWaypoint = 0f;
            }
        }
    }
}
