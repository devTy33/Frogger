using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) { //if statement to test collision with the player tag
            other.gameObject.transform.position = new Vector3(-10,-1,0); //if collision happens, move player back to beginning
        }
    }
}
