using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private bool levelComplete = false;
    private void Start()
    {

    }

    //if player touches flag, end the game.
    private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.name == "Player" && !levelComplete) {
                levelComplete = true;
                CompleteLevel();
            }
    }
    
    private void CompleteLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
