//code preloads scenes and then can add a loading scene if wanted 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int level;
    private int lives;
    private int score;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        NewGame();
    }

    //loads first level
    private void NewGame()
    {
        score = 0;

        LoadLevel(2);
    }
    //loading screen, right now loads instantly
    private void LoadLevel(int index)
    {
        level = index;

        Camera camera = Camera.main;

        
        //can have black screen as loading screen
        //currently loads instantly so no transisition
        if (camera != null) {
            camera.cullingMask = 0;
        }

        
        LoadScene();
    }

    //just loads scene from scene queue or based on scene index
    private void LoadScene()
    {
        SceneManager.LoadScene(level);
    }
    
    //when levels is completed load complete scene
    public void LevelComplete()
    {
        score += 1000;

        LoadLevel(level+1);
 
        
    }
    //restart level is game is failed
    public void LevelFailed()
    {
        
        LoadLevel(level);
    }

}
