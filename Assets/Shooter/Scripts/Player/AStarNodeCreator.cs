using System.Collections.Generic;
using UnityEngine;

public class AStarNodeCreator : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints where nodes should be created
    public LayerMask obstacleLayer; // Layer mask for obstacles

    public float nodeSpacing = 1f; // Spacing between nodes
    public bool createNodes = false;
    public GameObject nodeParent; // Parent object for PathNodes
    private List<PathNode> pathNodes = new List<PathNode>(); // List of created PathNodes

    void Update()
    {
        if (createNodes)
        {
            CreateNodes();
            createNodes = false;
        }
    }

    public void CreateNodes()
    {
        // Clear the existing PathNodes list
        pathNodes.Clear();

        // Create nodes between the first and last waypoints
        if (waypoints.Length >= 2)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Transform currentWaypoint = waypoints[i];
                Transform nextWaypoint = waypoints[(i + 1) % waypoints.Length]; // Circular loop

                CreateNodesBetweenWaypoints(currentWaypoint, nextWaypoint);
            }
        }
    }

    void CreateNodesBetweenWaypoints(Transform startWaypoint, Transform endWaypoint)
    {
        Vector3 direction = (endWaypoint.position - startWaypoint.position).normalized;
        float distance = Vector3.Distance(startWaypoint.position, endWaypoint.position);

        // Create the first node at the start waypoint
        CreateNode(startWaypoint.position);

        // Create nodes along the forward direction from the start waypoint up to the end waypoint
        for (float d = nodeSpacing; d < distance; d += nodeSpacing)
        {
            Vector3 nodePosition = startWaypoint.position + direction * d;

            // Perform raycasting from the node position to check for obstacles
            RaycastHit hit;
            if (Physics.Raycast(nodePosition, direction, out hit, nodeSpacing, obstacleLayer))
            {
                // Find the edges of the obstacle
                Vector3 rightEdge = Vector3.Cross(hit.normal, Vector3.up).normalized;
                Vector3 leftEdge = -rightEdge;

                // Create node paths along the edges of the obstacle
                CreateNodePath(nodePosition, rightEdge);
                CreateNodePath(nodePosition, leftEdge);

                // Skip to the next waypoint
                break;
            }
            else
            {
                // Create a node if no obstacle is detected
                CreateNode(nodePosition);
            }
        }

        // Create the last node at the end waypoint
        CreateNode(endWaypoint.position);
    }

    void CreateNodePath(Vector3 startingPosition, Vector3 edgeDirection)
    {
        Vector3 nodePosition = startingPosition + edgeDirection * nodeSpacing;
        while (!Physics.Raycast(nodePosition, Vector3.down, nodeSpacing, obstacleLayer))
        {
            CreateNode(nodePosition);
            nodePosition += edgeDirection * nodeSpacing;
        }
    }

    void CreateNode(Vector3 position)
    {
        // Create a parent object for PathNodes if it doesn't exist
        if (nodeParent == null)
        {
            nodeParent = new GameObject("PathNodes");
        }

        // Create a new node
        GameObject nodeObject = new GameObject("PathNode");
        nodeObject.transform.position = position;
        nodeObject.transform.parent = nodeParent.transform;

        PathNode newNode = nodeObject.AddComponent<PathNode>();
        newNode.ResetCosts();
        newNode.directions = GetDirections(); // Assign directions to the node

        // Add the node to the PathNodes list
        pathNodes.Add(newNode);
    }

    Vector3[] GetDirections()
    {
        // Define the four cardinal directions
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
        return directions;
    }
}