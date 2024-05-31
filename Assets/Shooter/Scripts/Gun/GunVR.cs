using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public enum FireMode { Auto, Burst, Single };
public class GunVR : MonoBehaviour
{

    #region Working Code
    //private void Start()
    //{
    //    grabInteractable = GetComponent<XRGrabInteractable>();
    //    grabInteractable.activated.AddListener(Fire);
    //}

    //public void Fire(ActivateEventArgs arg)
    //{
    //    GameObject spawnBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    //    spawnBullet.GetComponent<Projectiles>().SetSpeedDirection(fireSpeed, spawnBullet.transform.forward);
    //    _HandRecoil.ApplyRecoil();
    //    Play shoot sound
    //    if (shootSound != null)
    //    {
    //        AudioManager.instance.PlaySound(shootSound, transform.position);
    //    }

    //}
    #endregion


    // References
    public Animator _Animator; // Animator component for gun animations
    public GameObject bulletPrefab; // Prefab of the bullet
    public GameObject ArrowModel; // Cylinder GameObject (assuming it's part of the gun)
    public Transform spawnPoint; // Point where bullets are spawned
    public AudioClip shootSound; // Sound clip for shooting
    public float recoilForce = 0.1f; // Force applied to simulate recoil
    public XRGrabInteractable grabInteractable; // XRGrabInteractable component for grabbing the gun
    public RecoilHandler _HandRecoil; // Recoil handler for hand recoil
    //public XRSocketInteractor socketInteractor;

    // Shooting parameters
    public FireMode fireMode; // Fire mode (Single or Burst)
    public Transform[] projectileSpawn; // Array of projectile spawn points
    public Projectiles projectile; // Projectiles script for projectile behavior
    public float msBetweenShots = 100; // Time between shots in milliseconds
    public float muzzleVelocity = 1; // Velocity of the bullet
    public ParticleSystem muzzleEffect; // Muzzle flash effect
    public int burstCount; // Number of shots per burst
    public int projectilesPerMag; // Number of projectiles per magazine
    public float reloadTime = 5f; // Time taken to reload


    // Internal variables
    private Rigidbody rb; // Rigidbody component
    private bool isReloading; // Flag indicating whether the gun is reloading
    private int projectilesRemainingInMag; // Number of projectiles remaining in the magazine
    private float nextShotTime; // Time of the next shot

    ///  make logic when object get released from grab
    private bool isFlying = false;
    public Transform targetPosition;
    private float flySpeed = 2f;
    float timer = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(Fire);
        //grabInteractable.selectExited.AddListener(OnRelease);
        projectilesRemainingInMag = projectilesPerMag;

        ///if (socketInteractor == null)
        ///{
        ///    Debug.LogError("XR Socket Interactor is not assigned!");
        ///    enabled = false;
        ///}
    }

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
            rb.useGravity = false;
            // Calculate the direction towards the target position
            Vector3 direction = (targetPosition.position - transform.position).normalized;

            // Move the object towards the target position
            transform.position += direction * flySpeed * Time.deltaTime;

            // Check if the object has reached close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                transform.position = targetPosition.position;
                isFlying = false;
                rb.useGravity = true;
                timer = 0;
            }

            yield return null;
        }
    }


    void MoveToLocation(){
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

       
        if (isReloading)
        {
            return;
        }

        if (projectilesRemainingInMag == 0)
        {
            ArrowModel.SetActive(false);
            Reload();
        }
    }

    public void Fire(ActivateEventArgs arg)
    {
        if (Time.time > nextShotTime && projectilesRemainingInMag > 0)
        {
            if (fireMode == FireMode.Burst)
            {
                StartCoroutine(FireBurst());
            }
            else if (fireMode == FireMode.Single)
            {
                Shoot();
            }
            else if (fireMode == FireMode.Auto)
            {
                Shoot();
            }

            if (shootSound != null)
            {
                AudioManager.instance.PlaySound(shootSound, transform.position);
            }

            _HandRecoil.ApplyRecoil();
            projectilesRemainingInMag--;
            nextShotTime = Time.time + msBetweenShots / 1000;
        }else if(projectilesRemainingInMag <= 0)
        {
            //socketInteractor.socketActive = false;
            Debug.Log("No bullets");
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < projectileSpawn.Length; i++)
        {
            _Animator.SetTrigger("shot");
            GameObject proj = PoolManager.GetPoolManager().GetPoolObject(PoolObjectType.Arrow);
            proj.SetActive(true);
            proj.GetComponent<TrailRenderer>().enabled = true;
            proj.transform.position = projectileSpawn[i].position;
            proj.transform.forward = projectileSpawn[i].forward;
            proj.GetComponent<Projectiles>().SetSpeedDirection(muzzleVelocity, projectileSpawn[i].forward);
            Debug.Log("Instantiated ! projectile ");
        }
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < burstCount; i++)
        {
            Shoot();
            yield return new WaitForSeconds(msBetweenShots / 1000);
        }
    }

    private void Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            Debug.Log("Reloading...");

            Invoke("FinishReload", reloadTime);
        }
    }

    public void FinishReload()
    {
        projectilesRemainingInMag = projectilesPerMag;
        isReloading = false;
        if (!ArrowModel.activeSelf)
        {
            ArrowModel.SetActive(true);
        }
    }

    //private void RemoveAndDrop()
    //{
    //    Check if the interactor is currently holding an object
    //    if (socketInteractor.keepSelectedTargetValid)
    //    {
    //        Release the object from the interactor
    //        socketInteractor.keepSelectedTargetValid.GetComponent<XRGrabInteractable>().colliders.Clear();
    //        socketInteractor.DeactivateSocket();
    //        socketInteractor.selectTarget = null;

    //        Apply force to make the released object drop
    //        Rigidbody rb = socketInteractor.selectTarget.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.isKinematic = false;
    //            rb.AddForce(transform.forward * dropForce, ForceMode.Impulse);
    //        }
    //    }
    //}
}
