using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //if player lands on trap, he dies
        if (collision.gameObject.CompareTag("Trap")) {
            Die();
        }
    }

    private void Die() {
        //calls death animation
        rb.bodyType = RigidbodyType2D.Static;
        ani.SetTrigger("death");
    }

    private void RestartLevel() {
        //spawns player at beginning of level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
