using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DragStructures dragStructure;

    [SerializeField] private int slotIndex;
    public enum HoverType {
        UpgradeButton,
        Structure
    }

    public HoverType type;

    private void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData) {
        switch (type) { 
            case HoverType.UpgradeButton:
                Tooltip.ShowTooltip(GetUpgradeInfo());
                break;
            case HoverType.Structure:
                Tooltip.ShowTooltip(GetStructureInfo());
                break;
            default:
                Debug.LogError("Invalid string name of hoverObj");
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip();
    }

    //Gets the currently selected object's upgrade info based on upgrade slot index
    private string[] GetUpgradeInfo() {
        GameObject selectedObj = dragStructure.GetSelectedObject();
        if (selectedObj != null) { 
            StructureUpgradesInfo info = selectedObj.GetComponent<StructureUpgradesInfo>();
            if (info != null)
            {
                if (slotIndex != -1)
                {
                    string[] upgradeSlotInfo = info.GetUpgradeSlotInfo(slotIndex);
                    return upgradeSlotInfo;
                }
            }        
        }
        return null;
    }

    //Get the currently selected object's structure info
    private string[] GetStructureInfo() {
        Structure structure = gameObject.GetComponent<Structure>();
        if (structure != null)
        {
            string[] structureInfo = structure.GetStructureInfo();
            return structureInfo;

        }
        return null;
    }
}
