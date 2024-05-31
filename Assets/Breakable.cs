using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IDamageable
{
    public float _PossibleHits = 10; 

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Break(){
 
    }

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        //TakeDamage(damage);
        _PossibleHits -= damage;
        
        VFXManager.GetInstance().SpawningVFX(hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection), PoolObjectType.DamageTextVFX, EnemyPoints.GetPoints("EnemyType1"));
        if (_PossibleHits <= 0)
        {
            //TimedObjectSpawner.Instance.ReduceSpawnedObject();
            VFXManager.GetInstance().SpawningVFX(hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection), PoolObjectType.DamageVFX);
            PoolManager.GetPoolManager().CoolObject(gameObject, PoolObjectType.TargetPractice);
        }
    }

    public void TakeDamage(float damage)
    {
        
    }
}
