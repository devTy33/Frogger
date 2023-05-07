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

    private void NewGame()
    {
        score = 0;

        LoadLevel(1);
    }

    private void LoadLevel(int index)
    {
        level = index;

        Camera camera = Camera.main;

        // Don't render anything while loading the next scene to create
        // a simple scene transition effect
        if (camera != null) {
            camera.cullingMask = 0;
        }

        
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(level);
    }
    

    public void LevelComplete()
    {
        score += 1000;

        /*int nextLevel = level + 1;

        if (nextLevel < SceneManager.sceneCountInBuildSettings) {
            LoadLevel(nextLevel); */
        //}
         //else {
            LoadLevel(level+1);
        //}
        
    }

    public void LevelFailed()
    {
        
        LoadLevel(level);
    }

}
