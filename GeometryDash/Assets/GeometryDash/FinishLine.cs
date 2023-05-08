using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") { //Another collisions detecter to see if player collides
            SceneManager.LoadScene("GameOver"); //if player collides it goes to the game over scene
        }
    }
}
