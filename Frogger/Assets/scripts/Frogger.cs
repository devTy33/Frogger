
using UnityEngine;
using System.Collections;

public class Frogger : MonoBehaviour
{   
    public bool m_facingLeft = false;
    public bool m_facingRight = true;
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite leapSpriteRight;
    public Sprite leapSpriteLeft;
    public Sprite deadSprite;
    private Vector3 spawnPosition;
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }
    private void Update(){
        
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Move(Vector3.up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            Move(Vector3.down);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(m_facingLeft == false){
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
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("obstacle"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));//see if there's a platform layer
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("barier")); //see if there's a barrier at the destination
        if(barrier != null){
            return;
        }
        if(platform != null){
            transform.SetParent(platform.transform);    //set the parent so you move with the platform
        }
        else{
            transform.SetParent(null);
        }
        if(obstacle != null && platform == null){ //only die if you aren't on a platform
            transform.position = destination;
            Death();
        }
        else{
            StartCoroutine(Leap(destination));
        }
        //StartCoroutine(Leap(destination));

    }
    private IEnumerator Leap(Vector3 destination){
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;

        //spriteRenderer.sprite  = leapSprite;
        if(m_facingLeft == true) spriteRenderer.sprite = leapSpriteLeft;
        else{
            spriteRenderer.sprite = leapSpriteRight;
        }

        float t = elapsed / duration;
        while(elapsed < duration){
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;

        spriteRenderer.sprite = idleSprite;
        
    }
    public void Death(){
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deadSprite;
        enabled = false;
        FindObjectOfType<GameManager>().You_Died();

    }
    
    public void Respawn(){
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = idleSprite;
        transform.position = spawnPosition;
        gameObject.SetActive(true);
        enabled = true;
        

    }
    private void OnTriggerEnter2D(Collider2D other){
        if(enabled && other.gameObject.layer == LayerMask.NameToLayer("obstacle") && transform.parent == null){
            Death();
        }
        /*
        bool hitObstacle = other.gameObject.layer == LayerMask.NameToLayer("Obstacle");
        bool onPlatform = transform.parent != null;

        if (enabled && hitObstacle && !onPlatform) {
            Death();
        }
        */
    }
}
