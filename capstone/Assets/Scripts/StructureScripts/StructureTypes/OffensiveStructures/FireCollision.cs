using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject head = transform.parent.gameObject;
        GameObject turret = head.transform.parent.gameObject;
        GameObject flamethrower = turret.transform.parent.gameObject;
        Debug.Log(flamethrower.name + " line13");

        Debug.Log("AOE triggered");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy has entered the AOE zone");
            Emitter emitterScript = flamethrower.GetComponent<Emitter>();
            Debug.Log(other.gameObject);
            // AOEZone.Add(other.gameObject);
            // Debug.Log("Enemy has been added to list 1");
            emitterScript.AddToAOE(other.gameObject);
            Debug.Log("Enemy has been added to list 2");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject head = transform.parent.gameObject;
        GameObject turret = head.transform.parent.gameObject;
        GameObject flamethrower = turret.transform.parent.gameObject;

        Debug.Log("AOE triggered");
        if (other.CompareTag("Enemy"))
        {
            Emitter emitterScript = flamethrower.GetComponent<Emitter>();
            emitterScript.RemoveFromAOE(other.gameObject);
            Debug.Log("Enemy has exit the AOE zone");
        }
    }

}