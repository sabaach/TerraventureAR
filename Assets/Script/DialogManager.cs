using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogPanel;
    [SerializeField] bool initDialogOnStart;
    int dialogState;

    // Start is called before the first frame update
    void Start()
    {
        if (initDialogOnStart) StartDialog();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialog()
    {
        dialogState = 0;
        // dialogPanel.transform.GetChild(dialogState).gameObject.SetActive(true);
        dialogPanel.SetActive(true);
        Bounce(dialogPanel.transform.GetChild(dialogState).GetChild(1).gameObject);
    }

    public void NextDialog()
    {
        dialogPanel.SetActive(true);
        dialogPanel.transform.GetChild(dialogState).gameObject.SetActive(false);
        dialogState++;
        dialogPanel.transform.GetChild(dialogState).gameObject.SetActive(true);
        Bounce(dialogPanel.transform.GetChild(dialogState).GetChild(1).gameObject);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Ruangan3");
    }

    void Bounce(GameObject toBounce)
    {
        LeanTween.move(toBounce, toBounce.transform.position + Vector3.up * 20, 0.2f)/*.setDelay(0.5f)*/.setEase(LeanTweenType.easeOutBack);
        LeanTween.move(toBounce, toBounce.transform.position, 0.5f).setDelay(0.2f).setEase(LeanTweenType.easeOutBounce);
    }
}
