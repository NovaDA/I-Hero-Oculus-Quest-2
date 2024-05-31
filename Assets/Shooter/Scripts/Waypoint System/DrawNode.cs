using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class DrawNode : MonoBehaviour
{
    public GameObject targetObject;
    public Color wireColor = Color.red;
    public float sphereRadius = 1f;

    public void OnDrawGizmos()
    {
        if (targetObject == null)
            return;

        Gizmos.color = wireColor;
        Gizmos.DrawWireSphere(targetObject.transform.position, sphereRadius);
    }
}
