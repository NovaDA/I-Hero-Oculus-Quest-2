using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth { get; protected set; }
    public float health { get; protected set; }
    public bool dead;
    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }
    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        ///Debug.Log("Damage " + damage); right
        TakeDamage(damage);
    }

    
    public virtual void TakeDamage(float damage)
    {
        Debug.Log("Current Health " + health + "damage " + damage);
        health -= damage;
        
         if(health <= 0){
            //TimedObjectSpawner.Instance.ReduceSpawnedObject();
            Die();
        }
    }

    [ContextMenu("Self Destruct")]
    public virtual void Die()
    {
        dead = true;
        if(OnDeath != null){
            OnDeath();
        }
        //GameObject.Destroy(gameObject);
    }
}
