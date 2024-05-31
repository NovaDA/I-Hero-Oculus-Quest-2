using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public LayerMask targetMask;
    public SpriteRenderer dot;
    public Color dotHighLightColor;
    Color originalDotColor;

    private void Start()
    {
        Cursor.visible = false;
        originalDotColor = dot.color;
    }

    private void Update(){
        transform.Rotate(Vector3.forward * -40 * Time.deltaTime);
    }

    public void DetectTargets(Ray ray){
        if (Physics.Raycast(ray, 100, targetMask)){
            dot.color = dotHighLightColor;
        }
        else
        {
            dot.color = originalDotColor;
        }
    }
}
