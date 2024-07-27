using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetMessage : MonoBehaviour
{
    string currentHint;
    [SerializeField] TMP_Text notice;
    [SerializeField] Image textbox;
    public Sprite boxNormal, boxCorrect, boxWrong, boxHint;
    public enum textboxState
    {
        Normal,
        Correct,
        Wrong,
        Hint
    }

    public static SetMessage instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public void SetNotice(string message)
    {
        notice.text = message;
    }

    public void SetTextbox(textboxState state)
    {
        textbox.sprite = state switch
        {
            textboxState.Normal => boxNormal,
            textboxState.Correct => boxCorrect,
            textboxState.Wrong => boxWrong,
            textboxState.Hint => boxHint,
            _ => boxNormal,
        };
    }

    public void SetCurrentHint(string hint) => currentHint = hint;

    public void SetHint()
    {
        SetNotice(currentHint);
        SetTextbox(textboxState.Hint);
    }
}
