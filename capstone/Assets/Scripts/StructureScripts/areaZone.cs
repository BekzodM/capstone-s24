using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaZone : MonoBehaviour
{
    [SerializeField] private float areaEffectRadius = 10f;

    private void Start()
    {
        transform.localScale = new Vector3(areaEffectRadius, areaEffectRadius, areaEffectRadius);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObjectParent = transform.parent.gameObject;
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy has entered the trigger zone");
            Offensive offensiveScript = gameObjectParent.GetComponent<Offensive>();
            if (offensiveScript != null)
            {
                offensiveScript.StartAttacking(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject gameObjectParent = transform.parent.gameObject;
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy has left the trigger zone");
            Offensive offensiveScript = gameObjectParent.GetComponent<Offensive>();
            if (offensiveScript != null)
            {
                offensiveScript.StopAttacking(other.gameObject);
            }
        }

    }

    public float GetAreaEffectRadius()
    {
        return areaEffectRadius;
    }

    public void SetAreaEffectRadius(float radius)
    {
        areaEffectRadius = radius;
        transform.localScale = new Vector3(areaEffectRadius, areaEffectRadius, areaEffectRadius);
    }
}
