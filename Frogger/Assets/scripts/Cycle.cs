
using UnityEngine;

public class Cycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right;

    public float speed = 1f;
    public int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;
    //Allows the platform objects to rotate to the other side of the screen once they go out of frame
    private void Start(){
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);      
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }
    private void Update(){
        if(direction.x > 0 && (transform.position.x - size) > rightEdge.x){     //check if moving right and out of bounds
            Vector3 position = transform.position;          //set it's position to now be on left side
            position.x = leftEdge.x - size;
            transform.position = position;
        }
        else if(direction.x < 0 && (transform.position.x + size) < leftEdge.x){     //check if moving left and out of bounds
            Vector3 position = transform.position;          //set position to be on right side
            position.x = rightEdge.x + size;
            transform.position = position;
        }
        else{
            transform.Translate(direction * speed * Time.deltaTime);    //normal movement across screen
        }
    }
}
