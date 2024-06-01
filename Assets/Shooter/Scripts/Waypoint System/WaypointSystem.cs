using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    //public Transform[] waypoints;

    //private int currentWaypointIndex = 0;

    //public Vector3 GetNextWaypointPosition()
    //{
    //    if (waypoints == null || waypoints.Length == 0)
    //    {
    //        Debug.LogWarning("Waypoint array is empty or not assigned.");
    //        return Vector3.zero;
    //    }

    //    return waypoints[currentWaypointIndex].position;
    //}

    //public void SetNextWaypoint()
    //{
    //    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    //}

    //// Function to set a new waypoint array
    //public void SetWaypoints(Transform[] newWaypoints)
    //{
    //    waypoints = newWaypoints;
    //    currentWaypointIndex = 0; // Start from the first waypoint
    //}

    //public enum CurvatureAxis
    //{
    //    X,
    //    Y,
    //    Z
    //}

    //public Transform[] waypoints;
    //private List<Vector3> smoothedPath = new List<Vector3>();
    //private int currentPathIndex = 0;
    //public int segments = 10; // Number of segments for smoothing
    //public float curvatureSensitivity = 1.0f; // Sensitivity for the control point
    //public CurvatureAxis curvatureAxis = CurvatureAxis.Y; // Axis to apply curvature
    //public float gizmoSphereSize = 0.1f; // Size of the gizmo spheres

    //private void Start()
    //{
    //    if (waypoints != null && waypoints.Length > 0)
    //    {
    //        GenerateSmoothedPath();
    //    }
    //}

    //private void GenerateSmoothedPath()
    //{
    //    smoothedPath.Clear();

    //    for (int i = 0; i < waypoints.Length - 1; i++)
    //    {
    //        Vector3 p0 = waypoints[i].position;
    //        Vector3 p1 = waypoints[i + 1].position;
    //        Vector3 midPoint = (p0 + p1) / 2;
    //        Vector3 controlPoint = midPoint;

    //        switch (curvatureAxis)
    //        {
    //            case CurvatureAxis.X:
    //                controlPoint += new Vector3(curvatureSensitivity, 0, 0);
    //                break;
    //            case CurvatureAxis.Y:
    //                controlPoint += new Vector3(0, curvatureSensitivity, 0);
    //                break;
    //            case CurvatureAxis.Z:
    //                controlPoint += new Vector3(0, 0, curvatureSensitivity);
    //                break;
    //        }

    //        for (int j = 0; j <= segments; j++)
    //        {
    //            float t = j / (float)segments;
    //            Vector3 pointOnCurve = BezierCurve.CalculateQuadraticBezierPoint(t, p0, controlPoint, p1);
    //            smoothedPath.Add(pointOnCurve);
    //        }
    //    }

    //    // Optionally add the last waypoint to the path
    //    smoothedPath.Add(waypoints[waypoints.Length - 1].position);
    //}

    ////public Vector3 GetNextWaypointPosition()
    ////{
    ////    if (smoothedPath == null || smoothedPath.Count == 0)
    ////    {
    ////        Debug.LogWarning("Smoothed path is empty or not generated.");
    ////        return Vector3.zero;
    ////    }

    ////    return smoothedPath[currentPathIndex];
    ////}

    //public Vector3 GetNextWaypointPosition(Vector3 currentPosition)
    //{
    //    for (int i = 0; i < waypoints.Length; i++)
    //    {
    //        if (waypoints[i].position != currentPosition)
    //        {
    //            return waypoints[i].position;
    //        }
    //    }
    //    // If the current position is the last waypoint, return the first waypoint as the next position
    //    return waypoints[0].position;
    //}

    //public void SetNextWaypoint()
    //{
    //    currentPathIndex = (currentPathIndex + 1) % smoothedPath.Count;
    //}

    //// Function to set a new waypoint array
    //public void SetWaypoints(Transform[] newWaypoints)
    //{
    //    waypoints = newWaypoints;
    //    currentPathIndex = 0; // Start from the first waypoint
    //    if (waypoints != null && waypoints.Length > 0)
    //    {
    //        GenerateSmoothedPath();
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    if (smoothedPath == null || smoothedPath.Count == 0) return;

    //    Gizmos.color = Color.green;
    //    foreach (Vector3 point in smoothedPath)
    //    {
    //        Gizmos.DrawSphere(point, gizmoSphereSize);
    //    }
    //}



    public Transform[] waypoints;

    public enum CurvatureAxis
    {
        X,
        Y,
        Z
    }

    public int segments = 10; // Number of segments for smoothing
    public float curvatureSensitivity = 1.0f; // Sensitivity for the control point
    public CurvatureAxis curvatureAxis = CurvatureAxis.Y; // Axis to apply curvature
    public float gizmoSphereSize = 0.1f; // Size of the gizmo spheres

    private List<Vector3> smoothedPath = new List<Vector3>();

    public Vector3[] GenerateSmoothedPath()
    {
        smoothedPath.Clear();

        if (waypoints.Length < 2)
        {
            Debug.LogWarning("Not enough waypoints to generate a smoothed path.");
            return new Vector3[0];
        }

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Vector3 p0 = waypoints[i].position;
            Vector3 p1 = waypoints[i + 1].position;
            Vector3 midPoint = (p0 + p1) / 2;
            Vector3 controlPoint = midPoint;

            switch (curvatureAxis)
            {
                case CurvatureAxis.X:
                    controlPoint += new Vector3(curvatureSensitivity, 0, 0);
                    break;
                case CurvatureAxis.Y:
                    controlPoint += new Vector3(0, curvatureSensitivity, 0);
                    break;
                case CurvatureAxis.Z:
                    controlPoint += new Vector3(0, 0, curvatureSensitivity);
                    break;
            }

            for (int j = 0; j <= segments; j++)
            {
                float t = j / (float)segments;
                Vector3 pointOnCurve = BezierCurve.CalculateQuadraticBezierPoint(t, p0, controlPoint, p1);
                smoothedPath.Add(pointOnCurve);
            }
        }

        return smoothedPath.ToArray();
    }

    private void OnDrawGizmos()
    {
        if (smoothedPath == null || smoothedPath.Count == 0) return;

        Gizmos.color = Color.green;
        foreach (Vector3 point in smoothedPath)
        {
            Gizmos.DrawSphere(point, gizmoSphereSize);
        }
    }
}

