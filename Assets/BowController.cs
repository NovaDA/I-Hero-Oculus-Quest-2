using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowController : MonoBehaviour
{
    ///  make logic when object get released from grab
    private bool isFlying = false;
    public Transform targetPosition;
    private float flySpeed = 2f;
    float timer = 0.0f;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        //grabInteractable.selectExited.AddListener(OnRelease);

        ///if (socketInteractor == null)
        ///{
        ///    Debug.LogError("XR Socket Interactor is not assigned!");
        ///    enabled = false;
        ///}
    }
    // Start is called before the first frame update
    private void OnRelease(SelectExitEventArgs arg0)
    {
        Debug.Log("Object released!");
        // Add your logic here for when the object is releasedd
        isFlying = true;
        // Start flying coroutine
        StartCoroutine(FlyCoroutine());
    }

    private IEnumerator FlyCoroutine()
    {
        while (isFlying)
        {
            //rb.useGravity = false;
            // Calculate the direction towards the target position
            Vector3 direction = (targetPosition.position - transform.position).normalized;

            // Move the object towards the target position
            transform.position += direction * flySpeed * Time.deltaTime;

            // Check if the object has reached close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                transform.position = targetPosition.position;
                isFlying = false;
                //rb.useGravity = true;
                timer = 0;
            }

            yield return null;
        }
    }


    void MoveToLocation()
    {
        isFlying = true;
        StartCoroutine(FlyCoroutine());

    }


    private void Update()
    {
        if (!grabInteractable.isSelected)
        {

            timer += Time.deltaTime;
            if (Vector3.Distance(transform.position, targetPosition.position) > 0.01f || timer > 1f)
            {
                MoveToLocation();
            }

            // The grab interactable is not grabbed
            Debug.Log("Object is not grabbed");
        }
    }
}
