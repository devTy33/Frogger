using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        //makes camera follow player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
