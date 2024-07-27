using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action onDragRelease;
    Vector3 originPos, dragOffset;
    [SerializeField] bool returnOnRelese;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = transform.position - eventData.pointerCurrentRaycast.worldPosition;
        GetComponent<Collider>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = eventData.pointerCurrentRaycast.worldPosition + dragOffset;
        // transform.position = new(newPos.x, originPos.y + 0.01f, newPos.z);
        transform.position = newPos + transform.up * 0.01f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 newPos = eventData.pointerCurrentRaycast.worldPosition + dragOffset;
        // transform.position = new(newPos.x, originPos.y, newPos.z);
        transform.position = newPos;
        transform.localPosition = new(transform.localPosition.x, originPos.y, transform.localPosition.z);

        GetComponent<Collider>().enabled = true;
        if (returnOnRelese) transform.position = originPos;
        onDragRelease?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
