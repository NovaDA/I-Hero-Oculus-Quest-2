using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum State
{
    Patrolling,
    Chasing,
    Attacking,
    Idle,
    Flee,
    Dead
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : LivingEntity
{
    #region New Code

    //public State initialState = State.Patrolling;
    //public float patrolSpeed = 0.5f;
    //public float chaseSpeed = 1f;
    //public Transform[] patrolWaypoints;
    //public float waypointSwitchDelay = 2f;
    //public float detectionRange = 5f;
    //public float attackRange = 2f;

    //public ParticleSystem deathEffect;
    //public ParticleSystem damageEffect;
    //public static event Action OnDeathStatic;

    //private NavMeshAgent agent;
    //private State currentState;
    //private Transform target;
    //private Transform playerTarget;
    //private LivingEntity targetEntity;
    //public Material skinMaterial;
    //public Color originalColor = Color.black;
    //private float switchWaypointTimer = 0f;

    //private bool isTakingDamage = false;
    //private float damageStopTime = 0.5f;
    //public Image healthBar;
    //public TextMeshPro healthText;
    //private EnemyHitBehaviour hitBehaviour;

    //private float myCollisionRadius;
    //private float targetCollisionRadius;

    //private float attackDistanceThreshold = 0.5f;
    //private float timeBetweenAttacks = 1f;
    //private float nextAttackTime;
    //private int currentWaypointIndex;
    //private float damage = 1;
    //public int possibleHits = 1;
    //public int enemyHealth = 10;

    //private void Awake()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    hitBehaviour = GetComponent<EnemyHitBehaviour>();
    //    currentState = initialState;

    //    if (patrolWaypoints.Length > 0)
    //    {
    //        target = patrolWaypoints[0]; // Set initial target to the first waypoint
    //    }

    //    playerTarget = GameObject.FindGameObjectWithTag("Player").transform; // Assign player target

    //    if (playerTarget != null && playerTarget.GetComponent<LivingEntity>())
    //    {
    //        targetEntity = playerTarget.GetComponent<LivingEntity>();
    //        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
    //        targetCollisionRadius = playerTarget.GetComponent<CapsuleCollider>().radius;
    //        SetCharacteristics(0.5f, 10, enemyHealth, Color.white);

    //    }
    //    else
    //    {
    //        Debug.LogWarning("Player target or LivingEntity component not found!");
    //    }


    //}

    //protected override void Start()
    //{
    //    base.Start();

    //    Debug.Log("Start Method health " + health);

    //    if (healthText != null)
    //    {
    //        UpdateHealthText();
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Health UI Text reference is missing!");
    //    }

    //    if (playerTarget != null)
    //    {
    //        targetEntity.OnDeath += OnTargetDeath;
    //        StartCoroutine(UpdateState());
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Player target not found!");
    //    }
    //}

    //IEnumerator UpdateState()
    //{
    //    while (true) // Loop indefinitely
    //    {
    //        if (playerTarget != null && currentState != State.Flee) // Only update state if the player target exists
    //        {
    //            float distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

    //            // Check if the enemy is within attack range
    //            if (distanceToTarget <= attackRange)
    //            {
    //                currentState = State.Attacking;
    //            }
    //            // Check if the enemy is within detection range but not in attack range
    //            else if (distanceToTarget <= detectionRange)
    //            {
    //                currentState = State.Chasing;
    //            }
    //            // If the enemy is out of detection range, switch to patrolling mode
    //            else
    //            {
    //                currentState = State.Patrolling;
    //                target = GetNextWaypoint();
    //            }
    //        }

    //        switch (currentState)
    //        {
    //            case State.Patrolling:
    //                Patrol();
    //                break;
    //            case State.Chasing:
    //                if (playerTarget != null)
    //                    Chase();
    //                break;
    //            case State.Attacking:
    //                if (playerTarget != null)
    //                    Attack();
    //                break;
    //            case State.Flee:
    //                    Flee(); ;
    //                break;
    //        }

    //        yield return new WaitForSeconds(0.25f);
    //    }
    //}

    //void Flee()
    //{
    //    float minDistance = Mathf.Infinity;
    //    Transform bestEscapeRoute = null;

    //    foreach (GameObject escapeRoute in GameObject.FindGameObjectsWithTag("Escape"))
    //    {
    //        float distanceToEscape = Vector3.Distance(transform.position, escapeRoute.transform.position);

    //        // Check if the escape route is the nearest one
    //        if (distanceToEscape < minDistance)
    //        {
    //            minDistance = distanceToEscape;
    //            bestEscapeRoute = escapeRoute.transform;
    //        }
    //    }

    //    if (bestEscapeRoute != null)
    //    {
    //        // Set the escape route as the target and change the state to chasing
    //        target = bestEscapeRoute;
    //        currentState = State.Flee;
    //        agent.SetDestination(target.position);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No escape routes found!");
    //    }
    //}

    //void Patrol()
    //{
    //    if (patrolWaypoints.Length == 0)
    //    {
    //        Debug.LogWarning("No patrol waypoints assigned!");
    //        return;
    //    }

    //    if (agent.remainingDistance < 0.5f || switchWaypointTimer >= waypointSwitchDelay)
    //    {
    //        switchWaypointTimer = 0f;
    //        target = GetNextWaypoint();
    //        agent.SetDestination(target.position);
    //        agent.speed = patrolSpeed;
    //    }
    //}

    //void Chase()
    //{
    //    if (Vector3.Distance(transform.position, playerTarget.position) <= attackRange)
    //    {
    //        currentState = State.Attacking;
    //        return;
    //    }
    //    agent.SetDestination(playerTarget.position);
    //    agent.speed = chaseSpeed;
    //}

    //void Attack()
    //{
    //    if (Time.time > nextAttackTime)
    //    {
    //        float sqrtDestToTarget = (playerTarget.position - transform.position).sqrMagnitude;
    //        if (sqrtDestToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
    //        {
    //            nextAttackTime = Time.time + timeBetweenAttacks;
    //            AudioManager.instance.PlaySound("Enemy Attack", transform.position);
    //            StartCoroutine(AttackCoroutine());
    //        }
    //    }
    //}

    //IEnumerator AttackCoroutine()
    //{
    //    agent.enabled = false;
    //    currentState = State.Attacking;

    //    Vector3 originalPosition = transform.position;
    //    Vector3 dirToTarget = (playerTarget.position - transform.position).normalized;
    //    Vector3 attackPosition = playerTarget.position - dirToTarget * myCollisionRadius;   // + targetCollisionRadius + attackDistanceThreshold

    //    float attackSpeed = 3f;
    //    float percent = 0f;

    //    skinMaterial.color = Color.red;
    //    bool hasAppliedDamage = false;

    //    while (percent <= 1f)
    //    {
    //        if (percent >= 0.5f && !hasAppliedDamage)
    //        {
    //            hasAppliedDamage = true;
    //            targetEntity.TakeDamage(damage);
    //            possibleHits--;
    //        }

    //        percent += Time.deltaTime * attackSpeed;
    //        float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
    //        transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
    //        yield return null;
    //    }

    //    skinMaterial.color = originalColor;
    //    agent.enabled = true;

    //    currentState = State.Chasing;

    //    if (possibleHits <= 0)
    //    {
    //        currentState = State.Flee;
    //    }
    //}

    //private void OnTargetDeath()
    //{
    //    currentState = State.Patrolling;
    //}

    //public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    //{
    //    AudioManager.instance.PlaySound("Impact", transform.position);

    //    Debug.Log("Current Health" + health + " daMAGE " + damage);
    //    base.TakeHit(damage, hitPoint, hitDirection);

    //    if (damage >= health)
    //    {
    //        if (OnDeathStatic != null)
    //        {
    //            OnDeathStatic();
    //        }
    //        AudioManager.instance.PlaySound("Enemy Death", transform.position);
    //        Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.main.startLifetime.constantMax);
    //    }

    //    if (!isTakingDamage)
    //    {
    //        isTakingDamage = true;
    //        Destroy(Instantiate(damageEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, damageEffect.main.startLifetime.constantMax);
    //        StartCoroutine(DamageStopCoroutine(hitDirection));
    //    }

    //    UpdateHealthText();
    //}

    //IEnumerator DamageStopCoroutine(Vector3 hitDir)
    //{
    //    skinMaterial.color = Color.yellow;
    //    agent.enabled = false;
    //    hitBehaviour.HitByShot(hitDir);
    //    yield return new WaitForSeconds(damageStopTime);
    //    agent.enabled = true;
    //    isTakingDamage = false;
    //    skinMaterial.color = originalColor;
    //}

    //void UpdateHealthText()
    //{
    //    if (healthText != null)
    //    {
    //        healthText.text = "Health: " + health.ToString();
    //        float fillAmount = health / startingHealth;
    //        healthBar.fillAmount = fillAmount;
    //    }
    //}

    //public void SetCharacteristics(float moveSpeed, int hitToKillPlayer, float enemyHealth, Color skinColor)
    //{
    //    agent.speed = moveSpeed;
    //    startingHealth = enemyHealth;
    //    if (playerTarget != null)
    //    {
    //        //damage = Mathf.Ceil(targetEntity.startingHealth / hitToKillPlayer);
    //        //Debug.Log("Enemy health " + health);
    //    }

    //    Debug.Log("Enemy health " + health);

    //    skinMaterial.color = originalColor;
    //    var main = deathEffect.main;
    //    main.startColor = new Color(skinColor.r, skinColor.g, skinColor.b, 1);
    //    skinMaterial = GetComponent<Renderer>().material;
    //    originalColor = skinMaterial.color;
    //}

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, detectionRange);
    //}

    //Transform GetNextWaypoint()
    //{
    //    if (patrolWaypoints.Length == 0)
    //    {
    //        Debug.LogWarning("No patrol waypoints assigned!");
    //        return null;
    //    }

    //    int index = (currentWaypointIndex + 1) % patrolWaypoints.Length;
    //    currentWaypointIndex = index;
    //    return patrolWaypoints[index];
    //}
    #endregion

    public State initialState = State.Patrolling;
    public float patrolSpeed = 0.5f;
    public float chaseSpeed = 1f;
    public Transform[] patrolWaypoints;
    public float waypointSwitchDelay = 2f;
    public float detectionRange = 5f;
    public float attackRange = 2f;

    #region VFX NEED to to change this
    public ParticleSystem deathEffect;
    public ParticleSystem damageEffect;
    public static event Action OnDeathStatic;
    #endregion

    private NavMeshAgent agent;
    private State currentState;
    private Transform target;
    private Transform playerTarget;
    private LivingEntity targetEntity;
    // materials
    public SkinnedMeshRenderer skinMaterial;
    public Color originalColor;
    public Color damageColor;
    private float switchWaypointTimer = 0f;

    private bool isTakingDamage = false;
    private float damageStopTime = 0.5f;
    public Image healthBar;
    public TextMeshPro healthText;
    private EnemyHitBehaviour hitBehaviour;

    #region Animation
    AnimationController _AnimationController;
    private float myCollisionRadius;
    private float targetCollisionRadius;
    private float attackDistanceThreshold = 0.5f;
    private float timeBetweenAttacks = 1f;
    private float nextAttackTime;
    #endregion
    private int currentWaypointIndex;
    private float damage = 1;
    public int possibleHits = 1;
    public int enemyHealth = 10;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = skinMaterial.material.GetColor("_BaseColor");
        hitBehaviour = GetComponent<EnemyHitBehaviour>();
        _AnimationController = GetComponentInChildren<AnimationController>();
        currentState = initialState;

        if (patrolWaypoints.Length > 0){
            target = patrolWaypoints[0]; // Set initial target to the first waypoint
        }

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform; // Assign player target

        if (playerTarget != null && playerTarget.GetComponent<LivingEntity>()){
            targetEntity = playerTarget.GetComponent<LivingEntity>();
            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = playerTarget.GetComponent<CapsuleCollider>().radius;
            SetCharacteristics(0.5f, 10, enemyHealth, Color.white);

        }
        else{
            Debug.LogWarning("Player target or LivingEntity component not found!");
        }
    }

    protected override void Start()
    {
        base.Start();

        if (healthText != null)
        {
            UpdateHealthText();
        }
        else
        {
            Debug.LogWarning("Health UI Text reference is missing!");
        }

        if (playerTarget != null)
        {
            targetEntity.OnDeath += OnTargetDeath;
            StartCoroutine(UpdateState());
        }
        else
        {
            Debug.LogWarning("Player target not found!");
        }
    }

    IEnumerator UpdateState()
    {
        while (true) // Loop indefinitely
        {
            if (playerTarget != null && currentState != State.Flee && currentState != State.Idle
                && currentState != State.Dead) // Only update state if the player target exists
            {
                float distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

                // Check if the enemy is within attack range
                if (distanceToTarget <= attackRange)
                {
                    currentState = State.Attacking;
                }
                // Check if the enemy is within detection range but not in attack range
                else if (distanceToTarget <= detectionRange)
                {
                    currentState = State.Chasing;
                }
                // If the enemy is out of detection range, switch to patrolling mode
                else
                {
                    currentState = State.Patrolling;
                    target = GetNextWaypoint();
                }
            }

            switch (currentState)
            {
                case State.Patrolling:
                    Patrol();
                    _AnimationController.SetAnimationState(AnimationController.AnimationState.Idle, false);
                    _AnimationController.SetAnimationState(AnimationController.AnimationState.Move, true);
                    break;
                case State.Idle:
                    Idle();
                    _AnimationController.SetAnimationState(AnimationController.AnimationState.Move, false);
                    _AnimationController.SetAnimationState(AnimationController.AnimationState.Idle, true);
                    break;
                case State.Chasing:
                    if (playerTarget != null){
                        Chase();
                        _AnimationController.SetAnimationState(AnimationController.AnimationState.Idle, false);
                        _AnimationController.SetAnimationState(AnimationController.AnimationState.Move, true);
                    }  
                    break;
                case State.Attacking:
                    if (playerTarget != null){
                        Attack();
                        _AnimationController.SetAnimationState(AnimationController.AnimationState.Idle, false);
                        _AnimationController.SetAnimationState(AnimationController.AnimationState.Move, false);
                        _AnimationController.TriggerAnimation("Attack1");
                    }   
                    break;
                case State.Dead:
                    Idle();
                    break;
                case State.Flee:
                    Flee();
                    _AnimationController.SetAnimationState(AnimationController.AnimationState.Move, true);
                    break;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    void Flee()
    {
        float minDistance = Mathf.Infinity;
        Transform bestEscapeRoute = null;

        foreach (GameObject escapeRoute in GameObject.FindGameObjectsWithTag("Escape"))
        {
            float distanceToEscape = Vector3.Distance(transform.position, escapeRoute.transform.position);

            // Check if the escape route is the nearest one
            if (distanceToEscape < minDistance)
            {
                minDistance = distanceToEscape;
                bestEscapeRoute = escapeRoute.transform;
            }
        }

        if (bestEscapeRoute != null)
        {
            // Set the escape route as the target and change the state to chasing
            target = bestEscapeRoute;
            currentState = State.Flee;
            if (!agent.enabled) return;
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("No escape routes found!");
        }
    }
    void Patrol()
    {
        if (!agent.enabled) return; // Check if the agent is enabled

        if (patrolWaypoints.Length == 0)
        {
            Debug.LogWarning("No patrol waypoints assigned!");
            return;
        }

        if (agent.remainingDistance < 0.5f || switchWaypointTimer >= waypointSwitchDelay)
        {
            switchWaypointTimer = 0f;
            target = GetNextWaypoint();
            agent.SetDestination(target.position);
            agent.speed = patrolSpeed;
        }
    }
    void Chase()
    {
        if (!agent.enabled) return; // Check if the agent is enabled

        if (playerTarget != null && Vector3.Distance(transform.position, playerTarget.position) <= attackRange)
        {
            currentState = State.Attacking;
            return;
        }
        agent.SetDestination(playerTarget.position);
        agent.speed = chaseSpeed;
    }
    void Attack()
    {
        if (!agent.enabled) return; // Check if the agent is enabled

        if (Time.time > nextAttackTime)
        {
            float sqrtDestToTarget = (playerTarget.position - transform.position).sqrMagnitude;
            if (sqrtDestToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                AudioManager.instance.PlaySound("Enemy Attack", transform.position);
                StartCoroutine(AttackCoroutine());
            }
        }
    }
    void Idle()
    {
        agent.enabled = false;
    }
    IEnumerator AttackCoroutine()
    {
        agent.enabled = false;
        currentState = State.Attacking;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (playerTarget.position - transform.position).normalized;
        Vector3 attackPosition = playerTarget.position - dirToTarget * myCollisionRadius;   // + targetCollisionRadius + attackDistanceThreshold

        float attackSpeed = 3f;
        float percent = 0f;

        skinMaterial.material.SetColor("_BaseColor", Color.yellow);
        bool hasAppliedDamage = false;

        while (percent <= 1f)
        {
            if (percent >= 0.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
                possibleHits--;
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }

        skinMaterial.material.SetColor("_BaseColor", originalColor);
        agent.enabled = true;

        currentState = State.Chasing;

        if (possibleHits <= 0)
        {
            currentState = State.Flee;
        }
    }

    private void OnTargetDeath()
    {
        currentState = State.Patrolling;
    }


    // HERE ENEMY TAKES DAMAGE AND DIES
    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        AudioManager.instance.PlaySound("Impact", transform.position);
        base.TakeHit(damage, hitPoint, hitDirection);

        if (health <= 0)
        {
            if (OnDeathStatic != null)
            {
                OnDeathStatic();
            }
            _AnimationController.SetAnimationState(AnimationController.AnimationState.Move, false);
            _AnimationController.SetAnimationState(AnimationController.AnimationState.Die, true);
            currentState = State.Dead;
            AudioManager.instance.PlaySound("Enemy Death", transform.position);
            VFXManager.GetInstance().SpawningVFX(hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection), PoolObjectType.DeathVFX);
        }

        if (!isTakingDamage)
        {
            isTakingDamage = true;
            VFXManager.GetInstance().SpawningVFX(hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection), PoolObjectType.DamageTextVFX, EnemyPoints.GetPoints("EnemyType1"));
            VFXManager.GetInstance().SpawningVFX(hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection), PoolObjectType.DamageVFX);
            StartCoroutine(DamageStopCoroutine(hitDirection));
        }

        UpdateHealthText();
    }

    IEnumerator DamageStopCoroutine(Vector3 hitDir)
    {
        skinMaterial.material.SetColor("_BaseColor", Color.red);
        agent.enabled = false;
        hitBehaviour.HitByShot(hitDir);
        yield return new WaitForSeconds(damageStopTime);
        agent.enabled = true;
        isTakingDamage = false;
        skinMaterial.material.SetColor("_BaseColor", originalColor);
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
            float fillAmount = health / startingHealth;
            healthBar.fillAmount = fillAmount;
        }
    }

    public void SetCharacteristics(float moveSpeed, int hitToKillPlayer, float enemyHealth, Color skinColor)
    {
        agent.speed = moveSpeed;
        startingHealth = enemyHealth;
        var main = deathEffect.main;
        main.startColor = new Color(skinColor.r, skinColor.g, skinColor.b, 1);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    Transform GetNextWaypoint()
    {
        if (patrolWaypoints.Length == 0)
        {
            Debug.LogWarning("No patrol waypoints assigned!");
            return null;
        }

        int index = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        currentWaypointIndex = index;
        return patrolWaypoints[index];
    }
}