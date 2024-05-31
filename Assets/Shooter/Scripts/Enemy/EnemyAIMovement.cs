//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyAIMovement : MonoBehaviour
//{
//    public enum EnemyType { normal, tank,kamikaze }
//    public EnemyType enemyType;

//    //Reference of target to follow to activate the enemy
//    //Attach Player having NavMeshAgent to this component through unity inspector
//    [SerializeField] Transform TargetMovement;

//    //caching of NavMeshAgent for easier use
//    NavMeshAgent navMeshAgent;

//    //the range between target and enemy to activate the enemy
//    [SerializeField] float Range = 10f;
//    float distanceToTarget = Mathf.Infinity;

//    //Health and Speed characteristics of enemy
//    [SerializeField] int Damage = 10;
//    [SerializeField] float turningSpeed = 5f;

//    //Bool to know whether the enemy is activated or not
//    public bool isProvoked = false;

//    //bool to know whether the enemy is alive or not
//    bool dead;

//    /// Test Object
//    public Transform test;
//    public Player Player;
//    EnemyAIAttack AttackModule;

//    private void Awake(){

//        TargetMovement = Player.transform;
  
//    }
//    void Start(){
//        //caching navMeshagent
//        navMeshAgent = GetComponent<NavMeshAgent>();
//        AttackModule = GetComponent<EnemyAIAttack>();
//        StartCoroutine(GetRandomLocation());
//    }

//    void Update(){
//        //checking whether the enemy is still alive
//        //if not then return immediately
//        // need enemy health
//        dead = GetComponent<LivingEntity>().dead;
//        if (dead) { return; }

//        {
//            EngageTarget(TargetMovement, TargetMovement);
//        }
        
//        //assigning the distance to enemy from target
//        distanceToTarget = Vector3.Distance(TargetMovement.position, transform.position);
//    }

//    private void OnTriggerEnter(Collider other){
//        if(enemyType == EnemyType.range){
//            AttackModule.StartShoot();
//        }
//        else{
//            AttackModule.StartPhysicalAttack(other.transform);
//        }
        
//    }

//    private void OnTriggerExit(Collider other){
//        Debug.Log("Stop Action");
//        if(enemyType == EnemyType.range)
//        AttackModule.StopAction();
//    }

//    //Movement Function
//    private void EngageTarget(Transform movementTarget, Transform lookPosition){
//        //Look at direction of target function
//        lookTarget(lookPosition);

//        //compare distace to target and stopping distance that assigned in navmesh agent in target.
//        if (distanceToTarget >= navMeshAgent.stoppingDistance)
//        {
//            //if enemy haven't reached the stopping condition, move towards target
//            navMeshAgent.SetDestination(movementTarget.position);
//            //you can implement the animation codes for movement in here
//        }
//    }
//    //Look in direcetion of target
//    private void lookTarget(Transform lookPosition){
//        //calculate new direction vector from target's position to enemies position
//        Vector3 direction = (lookPosition.position - transform.position).normalized;

//        //make new qauternion with the new direction vector we calculated to assign that to the enemie's rotation
//        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

//        //assign the created quaternion to the enemy
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
//    }
//    IEnumerator GetRandomLocation(){
//        while(true)
//        {
//            Vector3 Pos = UnityEngine.Random.insideUnitCircle * 10;
//            test.transform.position = new Vector3(Pos.x, transform.position.y + 0.5f, Pos.y);
//            yield return new WaitForSeconds(10f);
//        } 
//    }


//    //gizmos to give a visual representation of range for debugging process
//    //for altering the range for a better gameplay.
//    private void OnDrawGizmosSelected(){
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, Range);
//    }
//}
