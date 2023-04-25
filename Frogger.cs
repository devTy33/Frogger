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
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine(Leap(destination));
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
}
