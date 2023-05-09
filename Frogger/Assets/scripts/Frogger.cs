
using UnityEngine;
using System.Collections;

public class Frogger : MonoBehaviour
{   
    public bool m_facingLeft = false;
    public bool m_facingRight = true;
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite leapSpriteRight;      //sprite facing right
    public Sprite leapSpriteLeft;       //sprite facing left
    public Sprite deadSprite;           //squished sprite
    private Vector3 spawnPosition;      //keep track of where you start for respawning 
   
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }
    //Use update for moving with arrow keys
    private void Update(){
        
        if(Input.GetKeyDown(KeyCode.UpArrow)){  //if you arrow key up you want to move up
            Move(Vector3.up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            Move(Vector3.down);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(m_facingLeft == false){                      //I use a sprite that is not symetrical so I have to orient it based off where it's already looking
                transform.rotation = Quaternion.Euler(0,180,0);
                m_facingLeft = true;
                m_facingRight = false;
            }
            Move(Vector3.left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(m_facingRight == false){
                transform.rotation = Quaternion.Euler(0,0,0);
                m_facingRight = true;
                m_facingLeft = false;
            }
            Move(Vector3.right);
        }

    }

    private void Move(Vector3 direction){
        //transform.position += direction;
        Vector3 destination = transform.position + direction;
        //Classify Layers so you can control the sprites movement (where it can and can't move)
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("obstacle"));       //activates if you are on said object
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));//see if there's a platform layer
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("barier")); //see if there's a barrier at the destination
        if(barrier != null){    //if you're colliding with a barrier you don't want to move
            return;
        }
        if(platform != null){
            transform.SetParent(platform.transform);    //set the parent so you move with the platform
        }
        else{
            transform.SetParent(null);
        }
        if(obstacle != null && platform == null){ //only die if you aren't on a platform and you're touching an obstacle (water)
            transform.position = destination;
            Death();
        }
        else{
            StopAllCoroutines();                //then it's a valid move
            StartCoroutine(Leap(destination));
        }

    }
    private IEnumerator Leap(Vector3 destination){
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;
        //make sure to use propper sprite image 
        if(m_facingLeft == true) spriteRenderer.sprite = leapSpriteLeft;
        else{
            spriteRenderer.sprite = leapSpriteRight;
        }

        //Give the leap a delay so it doesn't feel super fast and instant
        float t = elapsed / duration;
        while(elapsed < duration){
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;

        spriteRenderer.sprite = idleSprite;             //change leaping image to normal sprite after leap
        
    }
    public void Death(){
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;       //face origional
        spriteRenderer.sprite = deadSprite;             //use squished image
        enabled = false;                                //turn off control of sprite
        FindObjectOfType<GameManager>().You_Died();     //decrease lives and check if we need to end game

    }
    
    public void Respawn(){
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = idleSprite;
        transform.position = spawnPosition;         //return back to starting position
        gameObject.SetActive(true);
        enabled = true;                             //turn control back on
        

    }
    private void OnTriggerEnter2D(Collider2D other){        //other is the other collider you are touching
        if(enabled && other.gameObject.layer == LayerMask.NameToLayer("obstacle") && transform.parent == null){     //Die if you are touching an obstacle 
            Death();
        }
       
    }
}
