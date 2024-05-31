using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AmmoBag : MonoBehaviour
{
    public GameObject magazinePrefab;
    public Transform magazineSpawnPoint;
    private bool handInsideBag = false;
    private GameObject currentMagazineInstance;
    private Transform _HandPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right Hand") || other.CompareTag("Left Hand"))
        {
            handInsideBag = true;
            _HandPosition = other.transform.Find("Attach_LH");
            SpawnMagazine();
            Debug.Log("Hand in bag");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Right Hand") || other.CompareTag("Left Hand"))
        {
            handInsideBag = false;
            if (!IsMagazineGrabbed())
            {
                DestroyMagazine();
            }
        }
    }

    private bool IsMagazineGrabbed()
    {
        if (currentMagazineInstance != null)
        {
            XRGrabInteractable grabInteractable = currentMagazineInstance.GetComponent<XRGrabInteractable>();
            return grabInteractable != null && grabInteractable.isSelected;
        }
        return false;
    }

    private void SpawnMagazine()
    {
        // Instantiate the magazine prefab at the spawn point
        currentMagazineInstance = Instantiate(magazinePrefab, magazineSpawnPoint.position, magazineSpawnPoint.rotation);
        // Ensure the magazine has necessary components to be grabbable
        XRGrabInteractable grabInteractable = currentMagazineInstance.GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = currentMagazineInstance.AddComponent<XRGrabInteractable>();
        }
        grabInteractable.transform.SetParent(magazineSpawnPoint);
    }

    private void DestroyMagazine()
    {
        if (currentMagazineInstance != null)
        {
            currentMagazineInstance.transform.GetComponent<Ammo>().DestroyMagazine();
            currentMagazineInstance = null;
        }
    }

}
