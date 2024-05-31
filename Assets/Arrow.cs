using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public LayerMask collisionMask;
    public float speed = 10f;
    public float damage = 2;
    public Transform tips;

    private Rigidbody _rigidbody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;
        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _rigidbody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tips.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            CheckCollision();
            _lastPosition = tips.position;
        }
    }

    private void CheckCollision()
    {
        if(Physics.Linecast(_lastPosition, tips.position,out RaycastHit hitInfo))
        {

            if(hitInfo.transform.gameObject.layer != 8)
            {

                if (hitInfo.transform.gameObject.layer == 6)
                {
                    OnHitObject(hitInfo.collider, tips.position);
                }
                else if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidbody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidbody.velocity, ForceMode.Impulse);
                    
                }
                Stop();
            }
        }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = !usePhysics;
    }

    void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamageable damageableObj = c.GetComponent<IDamageable>();
        if (damageableObj != null)
        {
            damageableObj.TakeHit(damage, hitPoint, transform.forward);
            //delaying the despawn of the gameObject
            Debug.Log("Hit points" + hitPoint);
            //Invoke("ReturnPoolManager", 2f);
            //ReturnPoolManager();
        }
    }
}
