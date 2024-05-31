using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;

    public Vector3 GetNextWaypointPosition()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("Waypoint array is empty or not assigned.");
            return Vector3.zero;
        }

        return waypoints[currentWaypointIndex].position;
    }

    public void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    // Function to set a new waypoint array
    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        currentWaypointIndex = 0; // Start from the first waypoint
    }
}
