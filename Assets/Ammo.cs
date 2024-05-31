using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ammo : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        // Get the Rigidbody and XRGrabInteractable components
        rigidbodyComponent = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to the onSelectExit event
        //grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    public void OnRelease()
    {
        // When released, switch to gravity mode
        SetRigidbodyProperties(false, true);
    }

    private void SetRigidbodyProperties(bool isKinematic, bool useGravity)
    {
        if (rigidbodyComponent != null)
        {
            rigidbodyComponent.isKinematic = isKinematic;
            rigidbodyComponent.useGravity = useGravity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "cilinder" && !grabInteractable.isSelected)
        {
            // Return this instance to the pool manager
            Invoke("ReturnToPoolManger", 0.5f);
        }
    }

    void ReturnToPoolManger()
    {
        PoolManager.GetPoolManager().CoolObject(gameObject, PoolObjectType.Ammo);
        // Ensure to unsubscribe from the events to prevent memory leaks
        //grabInteractable.onSelectExited.RemoveListener(OnRelease);
    }

    public void Released()
    {
        StartCoroutine(StartTimer(5));
    }

    IEnumerator StartTimer(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Call the function when the timer runs out
        DestroyMagazine();
    }

    public void DestroyMagazine()
    {

        Destroy(this.gameObject);

    }

}
