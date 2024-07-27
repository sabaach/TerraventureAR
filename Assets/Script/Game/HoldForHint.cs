using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldForHint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float holdDuration = 0.2f;
    [SerializeField] CanvasGroup hintPanel;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(StartHolding());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        hintPanel.gameObject.LeanCancel();
        hintPanel.LeanAlpha(0, 0.2f);
        // LeanTween.alphaCanvas(hintPanel,0,0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator StartHolding()
    {
        yield return new WaitForSeconds(holdDuration);
        hintPanel.gameObject.LeanCancel();
        hintPanel.LeanAlpha(1, 0.2f).setDelay(0.2f);
        // hintPanel.SetActive(true);
    }
}
