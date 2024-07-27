using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KuisGiok2 : MonoBehaviour
{
    [SerializeField] Slider elementSlider;
    [SerializeField] string[] elementList;
    [SerializeField] string correctAnswer;
    [SerializeField] Button checkAnswer, nextQuestion;
    int selectedValue;

    SetMessage setMessage;
    EkspresiCecep ekspresiCecep;

    // Start is called before the first frame update
    void Start()
    {
        // Set the maximum value of the slider based on the number of elements
        elementSlider.maxValue = elementList.Length - 1;
        elementSlider.wholeNumbers = true;

        // Initialize the slider with the first element's name
        elementSlider.value = 1;
        selectedValue = 1;

        elementSlider.onValueChanged.AddListener(OnSliderValueChanged);

        checkAnswer.onClick.AddListener(CheckAnswer);

        setMessage = SetMessage.instance;
        ekspresiCecep = EkspresiCecep.instance;
        setMessage.SetCurrentHint("Bacalah teks pada kaca berjudul \"Penggolongan Mineral Logam\"");

        // Set initial notice
        checkAnswer.interactable = false;
        setMessage.SetNotice("Tarik garis menuju angka kekerasan yang tepat!");
        setMessage.SetTextbox(SetMessage.textboxState.Normal);
    }

    void OnSliderValueChanged(float value)
    {
        selectedValue = (int)value;
        //setMessage.SetNotice($"Apakah {elementList[selectedValue]} adalah lambang kimia yang kamu pilih?");
        setMessage.SetNotice("Ayo coba cari berapa tingkat kekerasan batu giok!");
        checkAnswer.interactable = true;
    }

    void CheckAnswer()
    {
        checkAnswer.onClick.RemoveAllListeners();
        checkAnswer.gameObject.SetActive(false);
        nextQuestion.gameObject.SetActive(true);

        string selectedElement = elementList[selectedValue];

        if (selectedElement == correctAnswer)
        {
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Senang);
            setMessage.SetNotice("Iya benar sekali! Batu giok memiliki tingkat kekerasan sebesar 7 Mohs");
            setMessage.SetTextbox(SetMessage.textboxState.Correct);
        }
        else
        {
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Sedih);
            setMessage.SetNotice("Sedikit lagi! Kekerasan batu giok adalah 7 Mohs");
            setMessage.SetTextbox(SetMessage.textboxState.Wrong);
        }

        FindAnyObjectByType<GoalManager>().AddObjectiveCount();
    }
}
