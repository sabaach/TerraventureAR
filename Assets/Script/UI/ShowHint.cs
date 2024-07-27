using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : MonoBehaviour
{
    SetMessage setMessage;

    // Start is called before the first frame update
    void Start()
    {
        setMessage = SetMessage.instance;
    }

    // Update is called once per frame
    public void ShowTheHint(string message)
    {
        setMessage.SetNotice(message);
        setMessage.SetTextbox(SetMessage.textboxState.Hint);
    }
}
