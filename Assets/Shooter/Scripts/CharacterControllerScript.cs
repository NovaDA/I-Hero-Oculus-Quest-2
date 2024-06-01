using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControllerScript : LivingEntity{
    #region OLD CODE
    //public float moveSpeed = 5f;
    //public float gravity = 20f; // Increased gravity
    //public float obstacleCheckDistance = 1f;
    //public WaypointSystem waypointSystem;

    //private CharacterController characterController;
    //private Vector3 moveDirection = Vector3.zero;

    //void Start()
    //{
    //    characterController = GetComponent<CharacterController>();
    //}

    //void Update()
    //{
    //    MoveToWaypoint();
    //}

    //void MoveToWaypoint()
    //{
    //    if (!waypointSystem.HasWaypoints())
    //        return;

    //    Vector3 targetPosition = waypointSystem.GetCurrentWaypointPosition();
    //    Vector3 direction = targetPosition - transform.position;

    //    // Check for obstacles between player and waypoint
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, direction, out hit, obstacleCheckDistance))
    //    {
    //        // If there's an obstacle, move in a direction perpendicular to the obstacle
    //        Vector3 perpendicularDirection = Vector3.Cross(Vector3.up, hit.normal);
    //        direction = perpendicularDirection.normalized;
    //    }

    //    // Move towards the waypoint
    //    moveDirection = direction.normalized * moveSpeed;

    //    // Apply gravity
    //    moveDirection.y -= gravity * Time.deltaTime;

    //    // Check if close to waypoint
    //    if (direction.magnitude < 0.1f)
    //    {
    //        waypointSystem.NextWaypoint();
    //    }

    //    // Apply movement
    //    characterController.Move(moveDirection * Time.deltaTime);
    //}
    #endregion

    ///last update 01/06/2024
    //public WaypointSystem waypointSystem;
    //public float moveSpeed = 5f;
    //public float gravity = 20f;
    //public float detectionRadius = 5f;
    //public LayerMask enemyLayer;

    //private NavMeshAgent agent;
    //public  int currentWaypointIndex = 0;
    //private bool isEnemyNearby = false;
    //private WaitForSeconds checkInterval = new WaitForSeconds(1f); // Check for enemies every 1 seconds

    //public bool enableMovemnt = true;

    //AnimationController anim;
    //protected override void Start()
    //{
    //    startingHealth = 1;
    //    base.Start();

    //    agent = GetComponent<NavMeshAgent>();
    //    anim = GetComponentInChildren<AnimationController>();
    //    SetNextWaypoint();
    //    StartCoroutine(MoveToWaypointCoroutine());
    //    StartCoroutine(CheckForEnemiesCoroutine());
    //    if(anim == null)
    //    {
    //        Debug.Log("animator is null");
    //    }
    //}

    //private IEnumerator MoveToWaypointCoroutine()
    //{
    //    while (true && !dead)
    //    {
    //        MoveToWaypoint();
    //        yield return null; // Wait until next frame
    //    }
    //}

    //private IEnumerator CheckForEnemiesCoroutine()
    //{
    //    while (true && !dead)
    //    {
    //        CheckForEnemies();
    //        yield return checkInterval; // Wait for the specified interval before checking again
    //    }
    //}

    //private void MoveToWaypoint()
    //{
    //    if (waypointSystem == null || currentWaypointIndex >= waypointSystem.waypoints.Length)
    //    {
    //        StopNavMeshAgent();
    //        return;
    //    }

    //    if (!enableMovemnt)
    //    {
    //        StopNavMeshAgent();
    //        return;
    //    }
    //    else
    //    {
    //        ResumeNavMeshAgent();
    //    }

    //    Vector3 directionToWaypoint = waypointSystem.waypoints[currentWaypointIndex].position - transform.position;
    //    // Get the character's forward direction in the horizontal plane
    //    Vector3 characterForward = transform.forward;
    //    characterForward.y = 0f; // Ignore the vertical component
    //    // Ignore the vertical component of the direction to the waypoint
    //    directionToWaypoint.y = 0f;
    //    // Calculate the signed angle between the character's forward direction and the direction to the waypoint
    //    float signedAngle = Vector3.SignedAngle(characterForward, directionToWaypoint, Vector3.up);
    //    // Check if the signed angle is within the threshold (-30 to 30 degrees)

    //    if (Mathf.Abs(signedAngle) <= 5f)
    //    {
    //        if (agent.enabled){
    //            // Move towards the waypoint if the angle is within the threshold
    //            ResumeNavMeshAgent();
    //        }
    //    }
    //    else
    //    {
    //        // Rotate towards the waypoint direction
    //        Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 40f * Time.deltaTime);
    //        if (!agent.enabled){
    //            // Stop moving if the angle is outside the threshold
    //            StopNavMeshAgent();
    //        } 
    //    }

    //    if (!agent.enabled)
    //        return;

    //    if (isEnemyNearby){  
    //        StopNavMeshAgent();
    //        return; // Stop further movement if an enemy is nearby
    //    }
    //    else{

    //        ResumeNavMeshAgent();
    //    }

    //    if (agent.remainingDistance < agent.stoppingDistance){
    //        currentWaypointIndex++;
    //        SetNextWaypoint();
    //    }
    //}

    ///// <summary>
    ///// !agent.pathPending && 
    ///// </summary>

    //private void SetNextWaypoint()
    //{
    //    if (currentWaypointIndex < waypointSystem.waypoints.Length)
    //    {
    //        agent.SetDestination(waypointSystem.waypoints[currentWaypointIndex].position);
    //    }
    //}

    //public void SetWaypointSystem(WaypointSystem newWaypointSystem)
    //{
    //    waypointSystem = newWaypointSystem;
    //    currentWaypointIndex = 0; // Start from the first waypoint
    //    SetNextWaypoint();
    //}

    //private void CheckForEnemies()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
    //    isEnemyNearby = colliders.Length > 0;
    //}

    //private void StopNavMeshAgent()
    //{
    //    anim.SetAnimationState(AnimationController.AnimationState.Move, false);
    //    anim.SetAnimationState(AnimationController.AnimationState.Idle, true);
    //    agent.isStopped = true;
    //}

    //private void ResumeNavMeshAgent()
    //{
    //    anim.SetAnimationState(AnimationController.AnimationState.Idle, false);
    //    anim.SetAnimationState(AnimationController.AnimationState.Move, true);
    //    agent.isStopped = false;
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //}

    //public override void Die()
    //{
    //    AudioManager.instance.PlaySound("Player Death", transform.position);
    //    anim.SetAnimationState(AnimationController.AnimationState.Move, false);
    //    anim.SetAnimationState(AnimationController.AnimationState.Idle, false);
    //    anim.SetAnimationState(AnimationController.AnimationState.Die, true);
    //    base.Die();
    //}

    public WaypointSystem waypointSystem;
    public float moveSpeed = 5f;
    public float gravity = 20f;
    public float detectionRadius = 5f;
    public LayerMask enemyLayer;

    private NavMeshAgent agent;
    private Vector3[] smoothedPath;
    private int currentWaypointIndex = 0;
    private bool isEnemyNearby = false;
    private WaitForSeconds checkInterval = new WaitForSeconds(1f); // Check for enemies every 1 seconds

    public bool enableMovement = true;

    private AnimationController anim;

    protected void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<AnimationController>();
        GenerateSmoothedPath();
        StartCoroutine(MoveToWaypointCoroutine());
        StartCoroutine(CheckForEnemiesCoroutine());
        if (anim == null)
        {
            Debug.Log("Animator is null");
        }
    }

    private void GenerateSmoothedPath()
    {
        if (waypointSystem != null)
        {
            smoothedPath = waypointSystem.GenerateSmoothedPath();
            currentWaypointIndex = 0;
            SetNextWaypoint();
        }
    }

    private IEnumerator MoveToWaypointCoroutine()
    {
        while (true && !dead)
        {
            MoveToWaypoint();
            yield return null; // Wait until next frame
        }
    }

    private IEnumerator CheckForEnemiesCoroutine()
    {
        while (true && !dead)
        {
            CheckForEnemies();
            yield return checkInterval; // Wait for the specified interval before checking again
        }
    }

    private void MoveToWaypoint()
    {
        if (smoothedPath == null || currentWaypointIndex >= smoothedPath.Length)
        {
            StopNavMeshAgent();
            return;
        }

        if (!enableMovement)
        {
            StopNavMeshAgent();
            return;
        }
        else
        {
            ResumeNavMeshAgent();
        }

        Vector3 destination = smoothedPath[currentWaypointIndex];
        agent.SetDestination(destination);

        if (isEnemyNearby)
        {
            StopNavMeshAgent();
            return; // Stop further movement if an enemy is nearby
        }
        else
        {
            ResumeNavMeshAgent();
        }

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            currentWaypointIndex++;
            SetNextWaypoint();
        }
    }

    private void SetNextWaypoint()
    {
        if (currentWaypointIndex < smoothedPath.Length)
        {
            agent.SetDestination(smoothedPath[currentWaypointIndex]);
        }
    }

    private void CheckForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        isEnemyNearby = colliders.Length > 0;
    }

    private void StopNavMeshAgent()
    {
        if (anim != null)
        {
            anim.SetAnimationState(AnimationController.AnimationState.Move, false);
            anim.SetAnimationState(AnimationController.AnimationState.Idle, true);
        }
        if (agent != null)
            agent.isStopped = true;
    }

    private void ResumeNavMeshAgent()
    {
        if (anim != null)
        {
            anim.SetAnimationState(AnimationController.AnimationState.Idle, false);
            anim.SetAnimationState(AnimationController.AnimationState.Move, true);
        }
        if (agent != null)
            agent.isStopped = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public override void Die()
    {
        AudioManager.instance.PlaySound("Player Death", transform.position);
        if (anim != null)
        {
            anim.SetAnimationState(AnimationController.AnimationState.Move, false);
            anim.SetAnimationState(AnimationController.AnimationState.Idle, false);
            anim.SetAnimationState(AnimationController.AnimationState.Die, true);
        }
        base.Die();
    }


    #region Old Code
    //private void MoveToWaypoint()
    //{
    //    if (isEnemyNearby)
    //    {
    //        anim.SetAnimationState(AnimationController.AnimationState.Move, false);
    //        anim.SetAnimationState(AnimationController.AnimationState.Idle, true);
    //        StopNavMeshAgent();
    //        return; // Stop further movement if an enemy is nearby
    //    }
    //    else
    //    {
    //        anim.SetAnimationState(AnimationController.AnimationState.Idle, false);
    //        anim.SetAnimationState(AnimationController.AnimationState.Move, true);
    //        ResumeNavMeshAgent();
    //    }

    //    if (waypointSystem == null || currentWaypointIndex >= waypointSystem.waypoints.Length)
    //        return;

    //    if (!agent.pathPending && agent.remainingDistance < 0.1f)
    //    {
    //        currentWaypointIndex++;
    //        SetNextWaypoint();
    //    }
    //}
    #endregion
}
