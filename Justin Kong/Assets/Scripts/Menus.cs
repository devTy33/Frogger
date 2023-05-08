//for start menu and end menu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    //start button just loads level
    public void HitPlay(){
        SceneManager.LoadScene(1);
    }

    //quit buttons quits out of everything
    public void HitQuit(){
        Application.Quit();
    }
}
