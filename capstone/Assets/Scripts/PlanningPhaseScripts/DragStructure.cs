using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragStructure : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private int prefabIndex;
    [SerializeField] private GameObject prefab;
    private GameObject instance;
    private bool isDragging = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        //structureInstance = Instantiate(structurePrefabs[prefabIndex]);
        instance = Instantiate(prefab);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            //structureInstance.transform.position = Input.mousePosition;
            //instance.transform.position = Input.mousePosition;
        }
    }
    private void Update()
    {
        if (isDragging) {
            instance.transform.position = Input.mousePosition;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
        }
    }
}
