using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilHandler : MonoBehaviour
{
    //public float recoilDistance = 0.05f;
    //public float recoilDuration = 0.1f;

    //private Vector3 originalLocalPosition;
    //private bool isRecoiling = false;
    //public void ApplyRecoil()
    //{
    //    if (isRecoiling)
    //        return;

    //    isRecoiling = true;
    //    originalLocalPosition = transform.localPosition;
    //    StartCoroutine(Recoil());
    //}

    //private IEnumerator Recoil()
    //{
    //    float elapsedTime = 0f;

    //    while (elapsedTime < recoilDuration)
    //    {
    //        float recoilAmount = Mathf.Lerp(0, recoilDistance, elapsedTime / recoilDuration);
    //        transform.localPosition = originalLocalPosition - Vector3.forward * recoilAmount;
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    // Reset position
    //    transform.localPosition = originalLocalPosition;
    //    isRecoiling = false;
    //}


    public float recoilDistance = 0.05f;
    public float recoilDuration = 0.1f;
    public float shakeIntensity = 0.05f;
    public float shakeDuration = 0.1f;

    private Vector3 originalLocalPosition;
    private Quaternion originalRotation;
    private bool isRecoiling = false;

    private void Start()
    {
        originalLocalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    public void ApplyRecoil()
    {
        if (isRecoiling)
            return;

        isRecoiling = true;
        StartCoroutine(Recoil());
        
    }

    private IEnumerator Recoil()
    {
        float elapsedTime = 0f;

        while (elapsedTime < recoilDuration)
        {
            float x = Random.Range(-shakeIntensity, shakeIntensity);
            float y = Random.Range(-shakeIntensity, shakeIntensity);
            float recoilAmount = Mathf.Lerp(0, recoilDistance, elapsedTime / recoilDuration);
            transform.localPosition = originalLocalPosition - Vector3.forward * recoilAmount;
            transform.localPosition +=  new Vector3(x, y, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset position
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalRotation;
        isRecoiling = false;
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.localPosition;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-shakeIntensity, shakeIntensity);
            float y = Random.Range(-shakeIntensity, shakeIntensity);

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset position
        transform.localPosition = originalPosition;
    }
}
