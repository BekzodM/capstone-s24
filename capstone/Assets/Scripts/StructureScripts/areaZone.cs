using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaZone : MonoBehaviour
{
    [SerializeField] private float areaEffectRadius = 10f;

    private void Start()
    {
        //transform.localScale = new Vector3(areaEffectRadius,areaEffectRadius,areaEffectRadius);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject structure = transform.parent.gameObject;
        string structureType = structure.GetComponent<Structure>().GetStructureType();

        if (other.CompareTag("Enemy"))
        {
            if (structureType == "Offensive") {
                HandleOnTriggerEnterForOffensiveStructures(structure, other.gameObject);
            }
            else if (structureType == "Defensive")
            {
                HandleOnTriggerEnterForDefensiveStructures(structure, other.gameObject);
            }
            else if (structureType == "Support")
            {
                HandleOnTriggerEnterForSupportStructures(structure, other.gameObject);
            }
            else if (structureType == "Trap")
            {
                HandleOnTriggerEnterForTrapStructures(structure, other.gameObject);
            }
            else
            {
                Debug.LogError("Invalid structure type");
            }
        }
        if (other.CompareTag("Structure")) {
            if (structureType == "Support") {
                GameObject ally;
                if (other.transform.parent.gameObject != null)
                {
                    ally = other.transform.parent.gameObject;
                }
                else {
                    ally = other.gameObject;
                }
                HandleOnTriggerEnterForSupportStructures(structure, ally);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject structure = transform.parent.gameObject;
        string structureType = structure.GetComponent<Structure>().GetStructureType();

        if (other.CompareTag("Enemy"))
        {
            if (structureType == "Offensive")
            {
                HandleOnTriggerExitForOffensiveStructures(structure, other.gameObject);
            }
            else if (structureType == "Defensive")
            {
                HandleOnTriggerExitForDefensiveStructures(structure, other.gameObject);
            }
            else if (structureType == "Support")
            {
                HandleOnTriggerExitForSupportStructures(structure, other.gameObject);
            }
            else if (structureType == "Trap")
            {
                HandleOnTriggerExitForTrapStructures(structure, other.gameObject);
            }
            else {
                Debug.LogError("Invalid structure type");
            }
        }
        if (other.CompareTag("Structure"))
        {
            if (structureType == "Support")
            {
                HandleOnTriggerExitForSupportStructures(structure, other.gameObject);
            }
        }

    }
    
    private void OnTriggerStay(Collider other)
    {
        
    }
    // Offensive
    private void HandleOnTriggerEnterForOffensiveStructures(GameObject structure, GameObject other) {
        Offensive offensiveScript = structure.GetComponent<Offensive>();
        if (offensiveScript != null)
        {
            offensiveScript.StartAttacking(other);
        }
    }

    private void HandleOnTriggerExitForOffensiveStructures(GameObject structure, GameObject other) {
        Offensive offensiveScript = structure.GetComponent<Offensive>();
        if (offensiveScript != null)
        {
            offensiveScript.StopAttacking(other);
        }
    }

    //Defensive

    private void HandleOnTriggerEnterForDefensiveStructures(GameObject structure, GameObject other) {
        Defensive defensiveScript = structure.GetComponent<Defensive>();
        if (defensiveScript != null)
        {
            defensiveScript.StartDefensiveAttack(other);
        }
    }

    private void HandleOnTriggerExitForDefensiveStructures(GameObject structure, GameObject other)
    {
        Defensive defensiveScript = structure.GetComponent<Defensive>();
        if (defensiveScript != null)
        {
            defensiveScript.EndDefensiveAttack(other);
        }
    }

    //Support
    private void HandleOnTriggerEnterForSupportStructures(GameObject structure, GameObject other) {
        Support supportScript = structure.GetComponent<Support>();
        if (supportScript != null) {
            supportScript.StartSupport(other);
        }
    }

    private void HandleOnTriggerExitForSupportStructures(GameObject structure, GameObject other) {
        Support supportScript = structure.GetComponent<Support>();
        if (supportScript != null)
        {
            supportScript.EndSupport(other);
        }
    }

    //Trap
    private void HandleOnTriggerEnterForTrapStructures(GameObject structure, GameObject other) {
        Trap trapScript = structure.GetComponent<Trap>();
        if (trapScript != null) {
            trapScript.StartAttacking(other);
        }
    }

    private void HandleOnTriggerExitForTrapStructures(GameObject structure, GameObject other) {
        Trap trapScript = structure.GetComponent<Trap>();
        if (trapScript != null)
        {
            trapScript.StopAttacking(other);
        }
    }

    public float GetAreaEffectRadius() {
        return areaEffectRadius;
    }

    public void SetAreaEffectRadius(float radius)
    {
        areaEffectRadius = radius;
        transform.localScale = new Vector3(areaEffectRadius, areaEffectRadius, areaEffectRadius);
    }


}
