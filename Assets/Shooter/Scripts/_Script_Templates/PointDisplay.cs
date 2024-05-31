using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    public TextMeshPro myText;
    private float moveSpeed = 2.0f;
    private Vector3 moveDir;
    private bool canMove = false;
    private float destroyTime = 2.0f;


    private void Start()
    {
        moveDir = Vector3.up + Random.insideUnitSphere * 0.5f;
    }

    private void Update()
    {
        if (canMove)
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        //destroyTime -= Time.deltaTime;
        //if (destroyTime <= 0)
        //{
        //    ReturnToPool();
        //}
    }

    public void ShowPoints(string points)
    {
        if (myText.text == null)
        {
            Debug.Log("TEXT UI");
            return;
        }
        myText.text = points;
        myText.color = Color.green;
        canMove = true;
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        canMove = false;
        destroyTime = 2.0f; // Reset destroyTime for reuse
    }
}
