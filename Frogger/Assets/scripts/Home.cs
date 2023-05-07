
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject frog;
    private void OnEnable(){
        frog.SetActive(true);
    }
    private void OnDisable(){
        frog.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other){        //Pther collider has entered trigger zone
        
        if(other.tag == "Player"){
            enabled = true;
            //Frogger frogger = other.GetComponent<Frogger>();
            //frogger.Respawn();
          
            FindObjectOfType<GameManager>().HomeReached();
        }
        
    }
   
}
