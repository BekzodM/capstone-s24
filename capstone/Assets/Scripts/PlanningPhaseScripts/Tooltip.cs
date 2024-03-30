using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Canvas parentCanvas;
    //public Transform tooltipTransform;
    public static Tooltip Instance { get; private set; }

    private RectTransform canvasRectTransform;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canvasRectTransform = parentCanvas.GetComponent<RectTransform>();
        backgroundRectTransform = transform.Find("UpgradesBackground").GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        
        // Add an offset to the anchored position
        float xOffset = 30f; // Adjust as needed
        float yOffset = 30f; // Adjust as needed
        anchoredPosition.x += xOffset;
        anchoredPosition.y += yOffset;

        // Ensure the tooltip stays within the canvas bounds
        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f, parentCanvas.pixelRect.width);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0f, parentCanvas.pixelRect.height);
        
        
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            //tooltup left screen on the right side
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            //tooltup left screen on the top side
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void Show() { 
        backgroundRectTransform.gameObject.SetActive(true);
    }

    private void Hide()
    {
        backgroundRectTransform.gameObject.SetActive(false);
    }

    public static void ShowTooltip() {
        Instance.Show();
    }

    public static void HideTooltip()
    {
        Instance.Hide();
    }

}
