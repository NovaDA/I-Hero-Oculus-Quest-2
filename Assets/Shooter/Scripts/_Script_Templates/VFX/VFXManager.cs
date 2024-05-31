using System;
using System.Collections;
using UnityEngine;

public class VFXManager : MonoBehaviour {

    /// <summary>
    /// I am using this class to handle the visual effects in the game
    /// </summary>
    /// 
    public string enemyPointDataFilePath = "Assets/EnemyPointData.txt";

    static VFXManager instance;

    public static VFXManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        EnemyPoints.InitializeFromTextFile(enemyPointDataFilePath);
    }

    public void SpawningVFX(Vector3 targetLocation, Quaternion rotation, PoolObjectType type)
    {
        if (targetLocation == null)
            return;

        // Retrieve VFX object from the pool
        GameObject vfx = PoolManager.GetPoolManager().GetPoolObject(type);
        vfx.transform.position = targetLocation;
        vfx.SetActive(true);

        // Start coroutine to check VFX status
        instance.StartCoroutine(CheckStatusVFX(vfx, type));
    }

    public void SpawningVFX(Vector3 targetLocation, Quaternion rotation, PoolObjectType type, string textToDisplay)
    {
        if (targetLocation == null)
            return;

        // Retrieve VFX object from the pool
        GameObject vfx = PoolManager.GetPoolManager().GetPoolObject(type);
        vfx.transform.position = targetLocation; 
        vfx.GetComponent<PointDisplay>().ShowPoints(textToDisplay);
        vfx.SetActive(true);

        // Start coroutine to check VFX status
        instance.StartCoroutine(CheckStatusVFX(vfx, type));
    }

    //private static PoolObjectType CheckType(string type)
    //{
    //    PoolObjectType poolType;
    //    // Attempt to parse the string into the PoolObjectType enum
    //    if (System.Enum.TryParse(type, out poolType))
    //    {
    //        // Use the parsed enum value
    //        Debug.Log("Parsed enum value: " + type);
    //    }
    //    else
    //    {
    //        // Handle invalid enum string
    //        Debug.LogWarning("Invalid enum string: " + type);
    //        poolType = PoolObjectType.DamageVFX; // Default to Error or handle differently as needed
    //    }

    //    return poolType;
    //}

    static IEnumerator CheckStatusVFX(GameObject vfx, PoolObjectType poolType)
    {
        ParticleSystem particleSystem = vfx.GetComponent<ParticleSystem>();

        while (particleSystem.isPlaying && vfx != null)
        {
            yield return new WaitForEndOfFrame();
        }

        PoolManager.GetPoolManager().CoolObject(vfx, poolType);
    }

}
