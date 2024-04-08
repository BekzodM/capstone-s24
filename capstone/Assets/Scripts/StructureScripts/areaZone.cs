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
        if (other.CompareTag("Enemy"))
        {
            string structureType = structure.GetComponent<Structure>().GetStructureType();
            if (structureType == "Offensive") {
                HandleOnTriggerEnterForOffensiveStructures(structure, other.gameObject);
            }
            else if (structureType == "Defensive")
            {
                HandleOnTriggerEnterForDefensiveStructures(structure, other.gameObject);
            }
            else if (structureType == "Support")
            {

            }
            else if (structureType == "Trap")
            {

            }
            else
            {
                Debug.LogError("Invalid structure type");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject structure = transform.parent.gameObject;
        if (other.CompareTag("Enemy"))
        {
            string structureType = structure.GetComponent<Structure>().GetStructureType();
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

            }
            else if (structureType == "Trap")
            {

            }
            else {
                Debug.LogError("Invalid structure type");
            }
        }

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

    public float GetAreaEffectRadius() {
        return areaEffectRadius;
    }

    public void SetAreaEffectRadius(float radius) { 
        areaEffectRadius = radius;
        transform.localScale = new Vector3(areaEffectRadius, areaEffectRadius, areaEffectRadius);
    }


}
