using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        Tooltip.ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip();
    }

}
