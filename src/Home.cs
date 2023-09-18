
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject frog;             //little frog image that appears when you reach home
    private void OnEnable(){
        frog.SetActive(true);
    }
    private void OnDisable(){
        frog.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other){        //other collider has entered trigger zone
        
        if(other.tag == "Player"){  //if a player enters the triger zone we change it to a little frog
            enabled = true;  
            FindObjectOfType<GameManager>().HomeReached();
        }
        
    }
   
}
