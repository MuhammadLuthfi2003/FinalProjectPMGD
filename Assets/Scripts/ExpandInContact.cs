using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExpandInContact : MonoBehaviour
{
    [SerializeField] GameObject expandObj;
    [SerializeField] float expandTime = .3f;
    [SerializeField] float scaleMultiplier = 1.2f;

    private bool isExpand = false;
    private float currentTimer = 0f;

    private void Update()
    {
        if (isExpand)
        {
            if (currentTimer > expandTime) { return; }
            currentTimer += Time.deltaTime;

            expandObj.transform.localScale = Vector3.Lerp(expandObj.transform.localScale, expandObj.transform.localScale * scaleMultiplier, expandTime * Time.deltaTime);
        }
        else
        {
            if (currentTimer > expandTime) { return; }
            currentTimer += Time.deltaTime;

            expandObj.transform.localScale = Vector3.Lerp(expandObj.transform.localScale, Vector3.one, expandTime * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isExpand = true;
            currentTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isExpand = false;
            currentTimer = 0f;
        }
    }
}
