using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GoalManager : MonoBehaviour
{
    [SerializeField] int target;
    int objectiveCount;
    public Action onObjectiveCompleted;
    [SerializeField] UnityEvent onCompleteEvent;
    [SerializeField] UnityEvent[] onIncreasedObjectiveEvent;
    [SerializeField] TMP_Text objectiveUI;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    void UpdateUI() => objectiveUI.text = $"{objectiveCount}/{target}";

    public void AddObjectiveCount()
    {
        objectiveCount++;
        UpdateUI();
        onIncreasedObjectiveEvent[objectiveCount - 1].Invoke();
        if (objectiveCount >= target)
        {
            onObjectiveCompleted?.Invoke();
            onCompleteEvent.Invoke();
        }
    }
}
