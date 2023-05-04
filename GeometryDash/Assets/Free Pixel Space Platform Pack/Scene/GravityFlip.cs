using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{

    private Rigidbody2D player;
    //private bool spaceDown;
    public float gravityForce;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (Input.GetKey(KeyCode.Space))
        {
            player.AddForce(Vector3.up * gravityForce, ForceMode2D.Force);
        } 
    }

}




