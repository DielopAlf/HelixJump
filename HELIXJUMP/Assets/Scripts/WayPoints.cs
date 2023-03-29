using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Comenzar en el primer waypoint
        transform.position = waypoints[currentWaypointIndex].position;
    }

    void Update()
    {
        // Mover hacia el siguiente waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        // Si alcanzamos el waypoint, avanzar al siguiente
        if (transform.position == waypoints[currentWaypointIndex].position)
        {
            currentWaypointIndex++;

            // Si llegamos al final, volver al principio
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}
