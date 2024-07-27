using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOne : MonoBehaviour
{
    [SerializeField] Toggle[] options;
    [TextArea()][SerializeField] string[] optionInfo;
    [SerializeField] int correctOption;
    int selectedOption;
    [SerializeField] Button confirm, nextQuestion;

    SetMessage setMessage;
    EkspresiCecep ekspresiCecep;

    // Start is called before the first frame update
    void Start()
    {
        setMessage = SetMessage.instance;
        ekspresiCecep = EkspresiCecep.instance;

        setMessage.SetNotice("Klik dan tahan jari pada batu untuk melihat jenisnya!");
        setMessage.SetTextbox(SetMessage.textboxState.Normal);
        ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Netral);
        setMessage.SetCurrentHint("Cermati teks dibawah gambar \"Terjadinya Mineral Logam\"");
        // nextQuestion.GetComponent<Image>().color = setMessage.boxNormal;

        for (int i = 0; i < options.Length; i++)
        {
            int index = i;
            options[index].onValueChanged.AddListener((active) => { SetInfo(active, optionInfo[index], index); });
        }
        confirm.onClick.AddListener(ConfirmChoice);
        confirm.interactable = false;
    }

    void SetInfo(bool active, string message, int thisOption)
    {
        if (active)
        {
            confirm.interactable = true;
            setMessage.SetNotice(message);
            selectedOption = thisOption;
        }
        else
        {
            confirm.interactable = false;
            setMessage.SetNotice("Klik dan tahan jari pada batu untuk melihat jenisnya!");
            setMessage.SetTextbox(SetMessage.textboxState.Normal);
        }
    }

    void ConfirmChoice()
    {
        if (selectedOption == correctOption)
        {
            setMessage.SetNotice("Benar sekali! Jawabannya adalah batuan beku!");
            setMessage.SetTextbox(SetMessage.textboxState.Correct);
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Senang);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxCorrect;
        }
        else
        {
            // options[correctOption].im
            setMessage.SetNotice("Sayang sekali, jawaban yang benar adalah batuan beku!");
            setMessage.SetTextbox(SetMessage.textboxState.Wrong);
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Sedih);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxWrong;
        }

        foreach (Toggle toggle in options)
        {
            toggle.interactable = false;
        }

        confirm.gameObject.SetActive(false);
        nextQuestion.gameObject.SetActive(true);

        FindAnyObjectByType<GoalManager>().AddObjectiveCount();
    }
}
