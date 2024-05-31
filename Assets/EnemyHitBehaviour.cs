using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Require Rigidbody component
public class EnemyHitBehaviour : MonoBehaviour
{
    public float hitForce = 100f; // Adjustable force to be applied when hit by a shot
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Ensure Rigidbody is kinematic initially
        rb.isKinematic = true;
    }

    public void HitByShot(Vector3 hitDirection)
    {
        // Enable Rigidbody to apply force
        rb.isKinematic = false;

        rb.AddForce(hitDirection.normalized * hitForce, ForceMode.Impulse);

        // Rotate towards hit direction
        RotateTowardsHitDirection(hitDirection);

        // Re-enable NavMeshAgent after a delay
        Invoke("EnableNavMeshAgent", 0.1f);
    }

    void RotateTowardsHitDirection(Vector3 hitDirection)
    {
        // Calculate rotation to face hit direction
        Quaternion targetRotation = Quaternion.LookRotation(hitDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust rotation speed as needed
    }

    void EnableNavMeshAgent()
    {
        // Re-enable NavMeshAgent and reset Rigidbody
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
