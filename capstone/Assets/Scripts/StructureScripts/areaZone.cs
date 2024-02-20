using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaZone : MonoBehaviour
{
    [SerializeField] private float areaEffectRadius = 10f;

    private void Start()
    {
        transform.localScale = new Vector3(areaEffectRadius,areaEffectRadius,areaEffectRadius);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObjectParent = transform.parent.gameObject;
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy has entered the trigger zone");
            Offensive offensiveScript = gameObjectParent.GetComponent<Offensive>();
            if (offensiveScript != null)
            {
                offensiveScript.StartShooting(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject gameObjectParent = transform.parent.gameObject;
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy has left the trigger zone");
            Offensive offensiveScript = gameObjectParent.GetComponent<Offensive>();
            if (offensiveScript != null)
            {
                offensiveScript.StopShooting(other.gameObject);
            }
        }

    }
}
