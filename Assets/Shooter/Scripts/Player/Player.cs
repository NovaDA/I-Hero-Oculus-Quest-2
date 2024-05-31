using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{

    public float moveSpeed = 5;
    //public CrossHair crossHair;
    Camera viewCamera;
    PlayerController controller;
    GunController gunController;


    // Start is called before the first frame update
    protected override void Start(){
        base.Start();  
    }

    private void Awake(){
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
        if(FindObjectOfType<Spawner>() != null)
        {
            FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
        }
        
    }

    void OnNewWave(int waveNumber){
        startingHealth = 10;
        gunController.EquipGun(waveNumber -1);
    }

    // Update is called once per frame
    void Update(){
        #region
        // movement Input
        // Changed the GetAxis to GeatAxisRaw to avoid the added smoothness of movement when a specific key is let go
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
        #endregion


        #region
        // look input
        // give a screen position, and it is interpreted from the camera to position clicked
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.up * gunController.GetHeight);
        float rayDistance;
        
        if (ground.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
            //if(crossHair != null){
            //    crossHair.transform.position = point;
            //    crossHair.DetectTargets(ray);
            //}
            
            if((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).magnitude > 1)
            {
                gunController.Aim(point);
            }
            


        }
        #endregion

        #region
        // Weapons input
        if (Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.OnTriggerRelease();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gunController.Reload();
        }
        #endregion

        if (transform.position.y < -10)
        {
            TakeDamage(health);
        }

    
    }

    public override void Die()
    {
        AudioManager.instance.PlaySound("Player Death", transform.position);
        base.Die();
    }
}
