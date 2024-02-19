using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObjectParent = transform.parent.gameObject;
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy has entered the trigger zone");
            Offensive offensiveScript = gameObjectParent.GetComponent<Offensive>();
            if (offensiveScript != null)
            {
                offensiveScript.CreateAttackProjectile(other.gameObject);
            }
        }
    }
}
