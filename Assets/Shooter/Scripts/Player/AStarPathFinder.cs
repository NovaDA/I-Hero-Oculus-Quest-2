using UnityEngine;
using System.Collections.Generic;

public class AStarPathfinder : MonoBehaviour
{
    public PathNode startNode;
    public PathNode endNode;

    private List<PathNode> openSet = new List<PathNode>();
    private HashSet<PathNode> closedSet = new HashSet<PathNode>();

    void Start()
    {
        FindPath(startNode, endNode);
    }

    void FindPath(PathNode start, PathNode end)
    {
        openSet.Clear();
        closedSet.Clear();

        openSet.Add(start);

        while (openSet.Count > 0)
        {
            PathNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == end)
            {
                // Path found
                return;
            }

            foreach (PathNode neighbor in currentNode.neighbors)
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float newGCost = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newGCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newGCost;
                    neighbor.hCost = GetDistance(neighbor, end);
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        // No path found
    }

    float GetDistance(PathNode nodeA, PathNode nodeB)
    {
        return Vector3.Distance(nodeA.transform.position, nodeB.transform.position);
    }
}
