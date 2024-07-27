using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KuisGiok1 : MonoBehaviour
{
    [SerializeField] Transform elementGroup;
    Toggle[] toggleGroup;
    List<Toggle> selectedToggle = new();
    [SerializeField] string[] elementList;
    [SerializeField] string[] correctAnswer;
    [SerializeField] Button checkAnswer, nextQuestion;
    int activeToggle;

    SetMessage setMessage;
    EkspresiCecep ekspresiCecep;

    //Benar = #C4B42B
    //Salah = #FA7D61

    // Start is called before the first frame update
    void Start()
    {
        List<Toggle> toggleList = new();
        for (int i = 0; i < elementGroup.transform.childCount; i++)
        {
            elementGroup.GetChild(i).name = elementList[i];
            elementGroup.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = elementList[i];

            if (elementGroup.GetChild(i).TryGetComponent(out Toggle toggle))
            {
                toggleList.Add(toggle);
                toggle.onValueChanged.AddListener((context) => SelectOption(toggle, context));
            }
        }
        // foreach (Transform toggleObj in elementGroup)
        // {
        //     if (toggleObj.TryGetComponent(out Toggle toggle))
        //     {
        //         toggleList.Add(toggle);
        //         toggle.onValueChanged.AddListener((_) => SelectOption(toggle));
        //         // if (correctAnswer.Contains(toggle.gameObject.name))
        //         // {
        //         //     toggle.onValueChanged.AddListener((_) => SelectOption(toggle));
        //         // }
        //     }
        // }
        toggleGroup = new Toggle[toggleList.Count];
        toggleList.CopyTo(toggleGroup);

        checkAnswer.onClick.AddListener(CheckAnswer);

        setMessage = SetMessage.instance;
        ekspresiCecep = EkspresiCecep.instance;
        setMessage.SetCurrentHint("Bacalah teks pada kaca berjudul \"Penggolongan Mineral Logam\"");
    }

    void SelectOption(Toggle toggle, bool on)
    {
        // print($"{toggle.name} is on: {on}. active toggle: {activeToggle}");
        if (on)
        {
            selectedToggle.Add(toggle);
            activeToggle++;
            if (activeToggle > 1)
            {
                selectedToggle[0].isOn = false;
            }
        }
        else
        {
            selectedToggle.Remove(toggle);
            activeToggle--;
        }
        if (activeToggle == 1)
        {
            checkAnswer.interactable = true;
            setMessage.SetNotice("Orang Indonesia suka koleksi batu giok atau tidak ya?");
            setMessage.SetTextbox(SetMessage.textboxState.Normal);

        }
        else
        {
            checkAnswer.interactable = false;
            setMessage.SetNotice("Yuk, pilih jawabanmu! Antara benar dan salah nih!");
            setMessage.SetTextbox(SetMessage.textboxState.Normal);

        }
    }

    void CheckAnswer()
    {
        checkAnswer.onClick.RemoveAllListeners();
        checkAnswer.gameObject.SetActive(false);
        nextQuestion.gameObject.SetActive(true);

        int point = 0;
        foreach (Toggle toggle in selectedToggle)
        {
            if (correctAnswer.Contains(toggle.name)) point++;
        }

        foreach (Toggle toggle in toggleGroup)
        {
            toggle.interactable = false;
        }

        if (point == 1)
        {
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Senang);
            setMessage.SetNotice("Pintar! Batu giok merupakan salah satu batu yang favorit orang Indonesia!");
            setMessage.SetTextbox(SetMessage.textboxState.Correct);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxCorrect;
        }
        else
        {
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Sedih);
            setMessage.SetNotice("Batu giok adalah salah satu batu favorit orang Indonesia!");
            setMessage.SetTextbox(SetMessage.textboxState.Wrong);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxWrong;
            // print("womp womp");
        }
        FindAnyObjectByType<GoalManager>().AddObjectiveCount();
    }
}
