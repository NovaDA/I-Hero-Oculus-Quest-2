using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform target;
    // Speed at which the PlayerPosition follows the target
    public float followSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   // public void Move(Vector3 _velocity)
   // {
       
    //}

    //public void LookAt(Vector3 lookPoint)
    //{
    //    Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
    //    transform.LookAt(heightCorrectedPoint);
    //}

    public void Update() {

        //transform.position = new Vector3(protection.position.x, transform.position.y, protection.position.z);
        //transform.rotation = new Quaternion()

        if (target == null)
        {
            Debug.LogWarning("Target not set for PlayerPosition.");
            return;
        }

        // Get the current position of PlayerPosition
        Vector3 currentPosition = transform.position;

        // Get the target position, but keep the Y position unchanged
        Vector3 targetPosition = new Vector3(target.position.x, currentPosition.y, target.position.z - 3f);

        // Smoothly interpolate between the current position and the target position
        transform.position = Vector3.Lerp(currentPosition, targetPosition, followSpeed * Time.deltaTime);
    }

}
 
