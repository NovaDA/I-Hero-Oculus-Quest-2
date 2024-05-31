using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{

    public enum FireMode { Auto, Burst, Single};
    public FireMode fireMode;
    public Transform[] projectileSpawn;
    public Projectiles projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    public int burstCount;
    public int projectilesPerMag;
    public float reloadTime = .3f;
    
    [Header("Recoil")]
    public Vector2 kickMinmax = new Vector2(.5f, 2f);
    public Vector2 recoilAngleMinmax = new Vector2(5,15);
    public float recoilMoveSettleSpeedTime  = .1f;
    public float recoilRotationSettleSpeedTime = .1f;

    [Header("Effects")]
    public Transform shell;
    public Transform shellEjection;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;
    MuzzleFlash muzzleFlash;
    float nextShotTime;

    bool triggerReleasedSinceLastShot;
    int shotsRemainingInBurst;
    int projectilesRemainingInMag;
    bool isReloading;

    Vector3 recoilSmoothDampVelocity;
    float recoilRotSmoothDampVelocity;
    float recoilAngle;

    private void Start(){
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        //grabInteractable.activated.AddListener(x => Shoot());

        muzzleFlash = GetComponent<MuzzleFlash>();
        shotsRemainingInBurst = burstCount;
        projectilesRemainingInMag = projectilesPerMag;

    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        // animate recoil
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, recoilMoveSettleSpeedTime);
        recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmoothDampVelocity, recoilRotationSettleSpeedTime);
        transform.localEulerAngles = transform.localEulerAngles + Vector3.left * recoilAngle;

        if (!isReloading && projectilesRemainingInMag == 0){
            Reload();
        }
    }
    void Shoot(){
        if(!isReloading && Time.time > nextShotTime && projectilesRemainingInMag > 0){
            if(fireMode == FireMode.Burst){
                if (shotsRemainingInBurst == 0)
                    return;
                shotsRemainingInBurst--;
            }else if(fireMode == FireMode.Single){
                if (!triggerReleasedSinceLastShot){
                    return;
                }
            }

            for (int i = 0; i < projectileSpawn.Length; i++){
                if (projectilesRemainingInMag == 0)
                    break;
                projectilesRemainingInMag--;
                nextShotTime = Time.time + msBetweenShots / 1000;
                Projectiles newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectiles;
                //newProjectile.SetSpeed(muzzleVelocity);
            }

            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleFlash.Activate();
            transform.localPosition -= Vector3.forward * Random.Range(kickMinmax.x, kickMinmax.y);
            recoilAngle = Random.Range(recoilAngleMinmax.x, recoilAngleMinmax.y);
            recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);

            AudioManager.instance.PlaySound(shootAudio, transform.position);

        }
    }

    public void Reload()
    {
        if(!isReloading && projectilesRemainingInMag != projectilesPerMag)
        StartCoroutine(AnimateReload());
        AudioManager.instance.PlaySound(reloadAudio, transform.position);
    }

    IEnumerator AnimateReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.2f);

        float reloadSpeed = 1f / reloadTime;
        float percent = 0;
        Vector3 initialRot = transform.localEulerAngles;
        float maxReloadAngle = 30f;

        while(percent < 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);
            transform.localEulerAngles = initialRot + Vector3.left * reloadAngle;
            yield return null;
        }

        isReloading = false;
        projectilesRemainingInMag = projectilesPerMag;
    }
    public void Aim(Vector3 aimPoint){
        if(!isReloading)
        transform.LookAt(aimPoint);
    }

    public void OnTriggerHold(){
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public void OnTriggerRelease(){
        triggerReleasedSinceLastShot = true;
    }

}
