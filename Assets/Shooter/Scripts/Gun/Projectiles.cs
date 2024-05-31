using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public LayerMask collisionMask;
    public Color trailColor; 
    Vector3 direction;
    float speed;
    float damage = 1;
    float lifeTime = 2;
    bool directionSet = false;
    bool hasCollided = false;

    float skinWidth = .1f;
    private Coroutine lifeCoroutine;
    Rigidbody rb;

    TrailRenderer trail;
    private float sphereRadius = 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }
        trail = GetComponent<TrailRenderer>();
        StartCoroutine(CheckLifetime());
    }
    /// <summary>
    ///  removed .material.SetColor("_TintColor", trailColor);
    /// </summary>
    private void OnEnable()
    {
        // Start the lifetime coroutine when the projectile is enabled
        lifeCoroutine = StartCoroutine(CheckLifetime());
    }

    private void OnDisable()
    {
        // Stop the lifetime coroutine when the projectile is disabled
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }
    }

    private IEnumerator CheckLifetime()
    {
        // Wait for the specified lifetime duration
        yield return new WaitForSeconds(lifeTime);

        // After the lifetime expires, return the projectile to the pool manager
        ReturnPoolManager();
    }

    public void SetSpeedDirection(float newSpeed, Vector3 Dir)
    {
        if(direction != Dir)
        {
            Debug.Log("New Speed " + newSpeed);
            speed = newSpeed;
            direction = Dir;
            directionSet = true;
            rb.AddForce(Dir * speed, ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (directionSet && !hasCollided){
            CheckCollisions(speed);
        }
    }

    private void CheckCollisions(float moveDistance){
        Ray ray = new Ray(transform.position, transform.forward);

        #region
        //RaycastHit[] hits;

        ///// QueryTriggerInteraction set what type of interaction happens and on which layer even if the collider is set to [Trigger]
        //if (Physics.Raycast(ray, out RaycastHit hit, 10f, collisionMask, QueryTriggerInteraction.Collide))
        //{
        //    hasCollided = true;
        //    hits = Physics.RaycastAll(ray, 0.5f, collisionMask, QueryTriggerInteraction.Collide);
        //    Debug.Log("Size of list arrays" + hits.Length);
        //    foreach (RaycastHit hited in hits)
        //    {
        //        //Debug.Log("Hit ");
        //        OnHitObject(hited.collider, hited.point);
        //        //moveDistance + skinWidth
        //    }
        //}
        #endregion

        RaycastHit[] hits = Physics.RaycastAll(ray, 0.8f, collisionMask, QueryTriggerInteraction.Collide);

        // Check if any collisions occurred
        if (hits.Length > 0)
        {
            hasCollided = true;

            Debug.Log("Number of hits: " + hits.Length);
            

            foreach (RaycastHit hit in hits)
            {
                transform.position = hit.point;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                rb.isKinematic = true;
                OnHitObject(hit.collider, hit.point);
            }
        }
        else
        {
            hasCollided = false;
        }
    }

    void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamageable damageableObj = c.GetComponent<IDamageable>();
        if (damageableObj != null)
        {
            damageableObj.TakeHit(damage, hitPoint, transform.forward);
            //delaying the despawn of the gameObject
            Debug.Log("Hit points" + hitPoint);
            Invoke("ReturnPoolManager", 2f);
            //ReturnPoolManager();
        }
    }

    void ReturnPoolManager(){
        rb.velocity = Vector3.zero;
        directionSet = false;
        hasCollided = false;
        trail.Clear();
        PoolManager.GetPoolManager().CoolObject(gameObject, PoolObjectType.Bullet);
    }


}
