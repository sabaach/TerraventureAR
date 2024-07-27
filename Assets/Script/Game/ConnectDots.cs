using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectDots : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UILineRenderer lineRenderer;
    RectTransform rectTransform;
    // [SerializeField] bool startFromHere;
    public int side;
    public bool taken;
    public ConnectDots connectedTo;
    ConnectDotsManager connectDotsManager;
    Vector2 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = (RectTransform)transform;
        connectDotsManager = FindAnyObjectByType<ConnectDotsManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void OnBeginDrag(PointerEventData eventData)
    {
        if (connectedTo != null)
        {
            // await Task.Run(() =>
            // {
                connectDotsManager.RecycleLineRenderer(lineRenderer);
                taken = connectedTo.taken = false;
                connectedTo.lineRenderer = lineRenderer = null;
                connectedTo.connectedTo = connectedTo = null;
                connectDotsManager.ChangeProgress(-1);
            // });
        }

        initialPos = eventData.pointerCurrentRaycast.screenPosition;

        // eventData.pointerCurrentRaycast.screenPosition
        // RectTransform rectLine = (RectTransform)lineRenderer.transform;
        // rectLine.anchoredPosition = rectTransform.anchoredPosition;
        await Task.Run(() =>
        {
            lineRenderer = connectDotsManager.GetUILineRenderer();
            lineRenderer.points = new Vector2[2];
        });
        lineRenderer.points[0] = rectTransform.anchoredPosition;
        lineRenderer.points[1] = rectTransform.anchoredPosition;
        lineRenderer.SetAllDirty();
        // startFromHere = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // if (eventData.pointerCurrentRaycast.gameObject.name[..3] != "Box" && startFromHere)
        if (lineRenderer != null)
        {
            // lineRenderer.points[1] = eventData.position - initialPos + lineRenderer.points[0];
            lineRenderer.points[1] = eventData.pointerCurrentRaycast.screenPosition - initialPos + lineRenderer.points[0];
            lineRenderer.SetAllDirty();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // startFromHere = false;
        if (!taken && eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ConnectDots connectDot) && connectDot.side != side && !connectDot.taken)
        {
            print("connected");
            connectedTo = connectDot;
            connectDot.connectedTo = this;
            taken = connectDot.taken = true;

            connectDot.lineRenderer = lineRenderer;

            RectTransform target = (RectTransform)connectDot.transform;
            lineRenderer.points[1] = target.anchoredPosition;
            lineRenderer.SetAllDirty();
            connectDotsManager.ChangeProgress(1);
            // enabled = false;
        }
        else
        {
            connectDotsManager.RecycleLineRenderer(lineRenderer);
            lineRenderer = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // if (!taken && !startFromHere && lineRenderer.points.Length > 0)
        // {
        //     print("ok");
        //     lineRenderer.points[1] = rectTransform.anchoredPosition;
        //     lineRenderer.SetAllDirty();
        // }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
