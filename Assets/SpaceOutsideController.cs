using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SpaceOutsideController : MonoBehaviour
{
    public XRLever _Lever;
    public XRKnob _Knob;

    public float _ForwardSpeed;
    public float _SideSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardVelocity = _ForwardSpeed * (_Lever.value ? 1 : 0);
        float sideVelocity = _SideSpeed * (_Lever.value ? 1 : 0) * Mathf.Lerp(-1, 1, _Knob.value);

        Vector3 velocity = new Vector3(sideVelocity, 0, forwardVelocity);
        transform.position += velocity * Time.deltaTime;
    }
}
