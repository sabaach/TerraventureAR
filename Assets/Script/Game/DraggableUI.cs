using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action onDragRelease;
    [HideInInspector] public PointerEventData pointerEvent;
    Vector2 dragOffset;
    [SerializeField] bool returnOnRelese;
    Vector2 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = (Vector2)transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)transform.position - eventData.position;
        GetComponent<Image>().raycastTarget = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + dragOffset;
        pointerEvent = eventData;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        if (returnOnRelese) transform.position = originPos;
        onDragRelease?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
