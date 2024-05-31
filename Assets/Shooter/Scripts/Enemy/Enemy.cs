using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking, TakingDamage };
    State currentState;

    public ParticleSystem deathEffect;
    public ParticleSystem damageEffect;
    public static event System.Action OnDeathStatic;
    #region Enemy Movement
    // Enemy Movement
    NavMeshAgent pathFinder;
    Transform target;
    LivingEntity targetEntity;
    public Material skinMaterial;
    public Color originalColor = Color.black;
    #endregion

    #region Close Range Attack
    float attackDistanceThreshold = .5f;
    float timeBetweenAttacks = 1;
    float nextAttackTime;
    float damage = 1;

    float myCollisionRadius;
    float targetCollisionRadius;
    #endregion

    bool hasTarget;
    bool isTakingDamage = false;
    float damageStopTime = 0.5f; // Adjust as needed
    public Image healthBar; // Reference to the UI Image element representing the health bar
    public TextMeshPro healthText;
    EnemyHitBehaviour hitBehaviour;

    private void Awake(){
        pathFinder = GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Player").transform != null){
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target.GetComponent<LivingEntity>()){
                targetEntity = target.GetComponent<LivingEntity>();
                myCollisionRadius = GetComponent<CapsuleCollider>().radius;
                targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
            }

        }

        SetCharacteristics(0.5f, 10, 10, Color.white);

        hitBehaviour = GetComponent<EnemyHitBehaviour>();
        if (hitBehaviour == null)
        {
            hitBehaviour = gameObject.AddComponent<EnemyHitBehaviour>();
        }
    }

    protected override void Start(){
        base.Start();

        if (hasTarget){
            currentState = State.Chasing;
            targetEntity.OnDeath += OnTargetDeath;
                StartCoroutine(PhysicalEnemyUpdatePath());
        }

        if (healthText != null)
        {
            UpdateHealthText();
        }
        else
        {
            Debug.LogWarning("Health UI Text reference is missing!");
        }
    }


    // Update the health UI text
    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
            // Calculate the fill amount based on current health
            float fillAmount = health / startingHealth;
            healthBar.fillAmount = fillAmount;
        }
    }



    public void SetCharacteristics(float moveSpeed, int hitToKillPlayer, float enemyHealth, Color skinColor){
        pathFinder.speed = moveSpeed;
        if (hasTarget){
            damage = Mathf.Ceil(targetEntity.startingHealth / hitToKillPlayer);
        }
        startingHealth = enemyHealth;
        skinMaterial.color = originalColor;
        var main = deathEffect.main;
        main.startColor = new Color(skinColor.r, skinColor.g, skinColor.b, 1);
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        AudioManager.instance.PlaySound("Impact", transform.position);
        
        if (damage >= health)
        {
            if (OnDeathStatic != null)
            {
                OnDeathStatic();
            }
            AudioManager.instance.PlaySound("Enemy Death", transform.position);
            Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.main.startLifetime.constantMax);
            
        }

        base.TakeHit(damage, hitPoint, hitDirection);
        
        if (!isTakingDamage) // Ensure enemy doesn't take damage repeatedly within damageStopTime
        {
            isTakingDamage = true;
            Destroy(Instantiate(damageEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, damageEffect.main.startLifetime.constantMax);
            StartCoroutine(DamageStopCoroutine(hitDirection));
        }

        UpdateHealthText(); // Update health UI text after taking dam

    }

    IEnumerator DamageStopCoroutine(Vector3 hitDir)
    {
        skinMaterial.color = Color.yellow;
        
        currentState = State.TakingDamage;
        pathFinder.enabled = false;
        // Apply hit behaviour
        hitBehaviour.HitByShot(hitDir);
        yield return new WaitForSeconds(damageStopTime);
        pathFinder.enabled = true;
        currentState = State.Chasing;
        isTakingDamage = false;
        skinMaterial.color = originalColor;
    }



    private void OnTargetDeath(){
        hasTarget = false;
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update(){
        if (hasTarget)
        {
             PhysicalAttack();
        } 
    }

    private void PhysicalAttack()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrtDestToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrtDestToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                AudioManager.instance.PlaySound("Enemy Attack", transform.position);
                StartCoroutine(Attack());

            }
        }
    }

    IEnumerator Attack(){
        currentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);


        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while(percent <= 1){
            if(percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;

        }

        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

    // Enemy will always move the stopping distance is set by the type
    IEnumerator PhysicalEnemyUpdatePath()
    {
        float refreshRate = .25f;
        while (hasTarget)
        {
            if (currentState == State.Chasing && !isTakingDamage)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

}
