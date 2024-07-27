using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KuisGamping3 : MonoBehaviour
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
            setMessage.SetNotice("Apakah lambang kimia yang kamu pilih sudah benar?");
            setMessage.SetTextbox(SetMessage.textboxState.Normal);

        }
        else
        {
            checkAnswer.interactable = false;
            setMessage.SetNotice("Ada sumber daya geologi apa saja ya di ruangan ini?");
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
            setMessage.SetNotice("Jenius! Batu gamping masuk ke dalam kategori mineral non logam");
            setMessage.SetTextbox(SetMessage.textboxState.Correct);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxCorrect;
        }
        else
        {
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Sedih);
            setMessage.SetNotice("Oops! Jawabanmu kurang tepat. Batu gamping tersusun dari mineral non logam");
            setMessage.SetTextbox(SetMessage.textboxState.Wrong);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxWrong;
            // print("womp womp");
        }
        FindAnyObjectByType<GoalManager>().AddObjectiveCount();
    }
}
