using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currIndex = 0;
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        //allows objects to follow waypoints to make traps move
        if (Vector2.Distance(waypoints[currIndex].transform.position, transform.position) < .1f) {
            currIndex++;
            if (currIndex >= waypoints.Length) {
                currIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currIndex].transform.position, Time.deltaTime * speed);
    }
}
