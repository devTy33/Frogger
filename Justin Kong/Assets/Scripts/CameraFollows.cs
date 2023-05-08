//code to follow charcter around
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
   
    // Update is called once per frame
    //follows charcter around
    void Update()
    {
        transform.position = target.position + offset ;
    }
}
