//Uses the CharacterController script to run these move commands
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f; //run speed of the scene

    float horizontalMove = 30f; //speed the character moves
    bool jump = false; 

    // Update is called once per frame
    void Update()
    {
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //This was used to controll the players movement both directions but 
                                                                       //with geometry dash you just continously go to the right so i commented it out
        if (Input.GetButtonDown("Jump")) { //if the scene picks up the up key pressed it'll cause the player to jump
            jump = true;
        }
    }

    void FixedUpdate () {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump); //used from the CharacterController script to get the movement for the player
        jump = false; //makes the jump false so he is not constantly jumping
    }
}
