using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MeteorDisable : MonoBehaviour
{

    public ParticleSystem _particles;

    public LayerMask _layerMask;
    public Transform _shootSource;
    public float _distance = 10;

    private bool _RayActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => StartShoot());
        grabInteractable.deactivated.AddListener(x => StopShoot());
    }

    private void StartShoot()
    {
        _particles.Play();
        _RayActivate = true;
    }
    private void StopShoot()
    {
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _RayActivate = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(_RayActivate)
            RaycastCheck();
    }

    void RaycastCheck()
    {
        RaycastHit hit;

        bool hasHit = Physics.Raycast(_shootSource.position, _shootSource.forward,
            out hit, _distance, _layerMask);

        if(hasHit)
        {
            hit.transform.gameObject.SendMessage("Break", SendMessageOptions.DontRequireReceiver);
        }
    }
}
