using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Typewrite : MonoBehaviour
{
    TMP_Text thisText;
    string fullContent;
    bool stillTyping;
    float delayPerChar = 0.015f;
    // [SerializeField] EventTrigger toBlock;
    [SerializeField] UnityEvent nextDialogEvent;

    // Start is called before the first frame update
    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        Retype();
    }

    public void Retype()
    {
        StartCoroutine(Typewriting());
    }

    public void ForceFinish()
    {
        // if (thisText.text != fullContent)
        // {
        //     StopAllCoroutines();
        //     thisText.text = fullContent;
        //     if (toBlock != null) toBlock.enabled = true;
        // }
        if (!stillTyping) nextDialogEvent.Invoke();
        stillTyping = false;
    }

    IEnumerator Typewriting()
    {
        int indexNow = 0;
        int totalLength = thisText.text.Length;
        fullContent = thisText.text;
        thisText.text = "";
        stillTyping = true;
        // if (toBlock != null) toBlock.enabled = false;
        while (indexNow < totalLength && stillTyping)
        {
            thisText.text += fullContent[indexNow];
            indexNow++;
            yield return new WaitForSeconds(delayPerChar);
        }
        thisText.text = fullContent;
        stillTyping = false;

        // if (toBlock != null) toBlock.enabled = true;
    }
}
