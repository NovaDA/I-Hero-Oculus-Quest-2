using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PathNode : MonoBehaviour
{
    public List<PathNode> neighbors = new List<PathNode>();
    public float gCost;
    public float hCost;

    public float FCost { get { return gCost + hCost; } }

    public Vector3[] directions; // Directions for pathfinding

    public void ResetCosts()
    {
        gCost = 1;
        hCost = 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one); // Draw a wire cube at the node position
    }
}
