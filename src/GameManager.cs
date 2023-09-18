using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    private Frogger frogger;
    private int lives;
    private Home[] homes;
    private int time;
    private int total_time;
    private int num_homes;
    public GameObject gameOverMenu;
    public GameObject winGameMenu;
    public Text lives_text;
    public Text time_score;
    public Text current_text;
    public Text winning_time;
  

    private void Awake(){
        homes = FindObjectsOfType<Home>();
        frogger = FindObjectOfType<Frogger>();
    }
    private void Start(){       //always start with 0 homes reach and calling a NewGame()
        num_homes = 0;
        NewGame();
    }
    private void NewGame(){
        num_homes  = 0;
        gameOverMenu.SetActive(false);

        SetLives(3);        //reset lives, time, and homes for each game
        NewLevel();
        total_time = 0;
    }
    private void End_Game(){
        frogger.gameObject.SetActive(false);            //turn of the frog
        if(num_homes == 5){                             //if we win we want to display the win screen and vise versa
            winning_time.text = total_time.ToString();  //set txt object for UI
            winGameMenu.SetActive(true);
        }
        else{
            gameOverMenu.SetActive(true);
        }

        StopAllCoroutines();
        StartCoroutine(PlayAgain());                //if they lose see if they want to play again

    }
    private IEnumerator PlayAgain(){                //Coroutine that keeps checking until the right key is pressed 
        bool play = false;
        while(!play){
            if(Input.GetKeyDown(KeyCode.Return)){
                play = true;
            }
            yield return null;
        }
        NewGame();
    }

    private void NewLevel(){                    //for a new level we want to reset the homes and respawn a new frog
        for(int i = 0; i < homes.Length; i++){
            homes[i].enabled = false;
        }
        Respawn();
    }

   
    private void Respawn(){                 //uses the respawn function from Frogger file
        frogger.Respawn();
        StopAllCoroutines();
        StartCoroutine(Timer(30));          //restarts timer at top of screen
    }
    private IEnumerator Timer(int dur){
        time = dur;
        current_text.text = time.ToString();
        while(time > 0){                      //timer counts down from 30
            yield return new WaitForSeconds(1);
            time--;
            current_text.text = time.ToString();
        }
        
        frogger.Death();                    //If timer gets to zero then frogger dies
    }

    private void SetTime(int tii){
        time_score.text = tii.ToString();   //set UI objects 
    }
    
    private void SetLives(int lives){
        this.lives = lives;
        lives_text.text = lives.ToString();
    }
   
    public void HomeReached(){
        frogger.gameObject.SetActive(false);
        num_homes++;
        total_time += (30 - time);      //math to reveal the elapsed time which is the score
        SetTime(total_time);
        if(num_homes == 5){             //game ends if all homes are filled
            Invoke(nameof(End_Game), 1f);   
        }
        if(Cleared()){
            Invoke(nameof(NewLevel), 1f);
        }
        else{
            Invoke(nameof(Respawn), 1f);    //if there are homes left to fill, respawn
        }
    }

    public void You_Died(){
        SetLives(lives - 1);
        if(lives > 0){
            Invoke(nameof(Respawn), 1f);
        }
        else{
            Invoke(nameof(End_Game), 1f);       //If you have no more lives left the you end the game
        }
    }

    private bool Cleared(){     //checks to see if all homes have been reached
        for(int i = 0; i < homes.Length; i++){
            if(!homes[i].enabled){
                return false;
            }
        }
        return true;
    }
   
}
