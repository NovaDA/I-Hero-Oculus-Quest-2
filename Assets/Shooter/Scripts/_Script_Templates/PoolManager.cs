using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Enemy_01,
    DamageVFX,
    DeathVFX,
    DamageTextVFX,
    TargetPractice,
    Bullet,
    Arrow,
    Ammo
}

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    public List<GameObject> pool = new List<GameObject>();
}


public class PoolManager : MonoBehaviour
{
    [SerializeField]
    List<PoolInfo> _listOfPool;
    private Vector3 defaultPos = new Vector3(-100, -100, -100);

    static PoolManager instance;

    public static PoolManager GetPoolManager(){
        return instance;
    }


    private void Awake()
    {
        instance = this;
    }
    void Start(){
        for (int i = 0; i < _listOfPool.Count; i++)
        {
            FillPool(_listOfPool[i]);
        }
    }

    void FillPool(PoolInfo info){
        for (int i = 0; i < info.amount; i++)
        {
            GameObject obInstance = null;
            obInstance = Instantiate(info.prefab, info.container.transform);
            obInstance.gameObject.SetActive(false);
            obInstance.transform.position = defaultPos;
            info.pool.Add(obInstance);

        }
    }

    public GameObject GetPoolObject(PoolObjectType type){
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        GameObject obInstance = null;
        if (pool.Count > 0)
        {
            obInstance = pool[pool.Count - 1];
            pool.Remove(obInstance);
        }
        else
        {
            obInstance = Instantiate(selected.prefab, selected.container.transform);
        }

        return obInstance;
    }

    public void CoolObject(GameObject ob, PoolObjectType type){
        

        if (!ob.activeSelf){
            Debug.LogWarning("Trying to cool an already inactive object: " + ob.name);
        }
        else{
            ob.SetActive(false);
        }

        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        if (!pool.Contains(ob)){
            if (ob.activeSelf)
                ob.SetActive(false);
            pool.Add(ob); 
        }
        else{
            Debug.LogWarning("Object " + ob.name + " already exists in the pool.");
        }
    }


    private PoolInfo GetPoolByType(PoolObjectType type){
        for (int i = 0; i < _listOfPool.Count; i++){
            if (type == _listOfPool[i].type){
                return _listOfPool[i];
            }
        }
        return null;
    }

}
