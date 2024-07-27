using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayButtonActive : MonoBehaviour
{
    [SerializeField] float delay;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        Deactivate();
    }

    public void ShowButton() => button.interactable = true;

    public void Deactivate()
    {
        button.interactable = false;
        CancelInvoke();
        Invoke(nameof(ShowButton), delay);
    }
}
