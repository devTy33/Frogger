using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{

    public Rigidbody2D player; //set this for the character (astronaut)
    public float gravityForce;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>(); //gets all of the rigid body information for the selected character
    }
    void Update() {
        if (Input.GetKey(KeyCode.Space)) //checks if space is pressed
        {
            player.AddForce(Vector3.up * gravityForce, ForceMode2D.Force); //if it is pressed, this command line will cause an upward force on the
        }                                                                  //player causing a floating "space walk" movement to fly above some obstacles
    }

}




