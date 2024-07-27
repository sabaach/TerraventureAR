using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ConnectDotsManager : MonoBehaviour
{
    [SerializeField] int maxPoint;
    int selectedOption;
    int point;
    [SerializeField] ConnectDots[] leftPair, correctRightPair;
    [SerializeField] Button confirm, nextQuestion;

    [Serializable]
    class LineList
    {
        public UILineRenderer lineRenderer;
        public bool available;
    }
    [SerializeField] LineList[] lineRenderers;

    SetMessage setMessage;
    EkspresiCecep ekspresiCecep;

    // Start is called before the first frame update
    void Start()
    {
        setMessage = SetMessage.instance;
        ekspresiCecep = EkspresiCecep.instance;

        setMessage.SetNotice("Tariklah garis antara benda dan mineral untuk menjawab!");
        setMessage.SetTextbox(SetMessage.textboxState.Normal);
        ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Netral);
        setMessage.SetCurrentHint("Mineral apa yang lebih berharga secara ekonomis? Cari jawabannya pada teks kaca dengan judul \"Logam Mulia\"");

        confirm.interactable = false;
        confirm.onClick.AddListener(CheckAnswer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public UILineRenderer GetUILineRenderer()
    {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            if (lineRenderers[i].available)
            {
                lineRenderers[i].available = false;
                return lineRenderers[i].lineRenderer;
            }
        }
        return lineRenderers[0].lineRenderer;
    }

    public void RecycleLineRenderer(UILineRenderer toBeRecycled)
    {
        // await Task.Run(() =>
        // {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            if (lineRenderers[i].lineRenderer == toBeRecycled)
            {
                lineRenderers[i].available = true;
                lineRenderers[i].lineRenderer.points = new Vector2[0];
                lineRenderers[i].lineRenderer.SetAllDirty();

            }
        }
        // });
    }

    public void ChangeProgress(int value)
    {
        selectedOption += value;
        if (selectedOption >= maxPoint)
        {
            setMessage.SetNotice("Apakah kamu sudah yakin?");
            setMessage.SetTextbox(SetMessage.textboxState.Normal);
            confirm.interactable = true;
        }
        else
        {
            setMessage.SetNotice("Tariklah garis antara benda dan mineral untuk menjawab!");
            setMessage.SetTextbox(SetMessage.textboxState.Normal);
            confirm.interactable = false;
        }
    }

    void CheckAnswer()
    {
        for (int i = 0; i < leftPair.Length; i++)
        {
            if (leftPair[i].connectedTo == correctRightPair[i])
            {
                point++;
            }
            leftPair[i].enabled = false;
            correctRightPair[i].enabled = false;
        }
        if (point >= maxPoint)
        {
            setMessage.SetNotice("Jawabanmu benar semua!");
            setMessage.SetTextbox(SetMessage.textboxState.Correct);
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Senang);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxCorrect;
        }
        else
        {
            setMessage.SetNotice("Emas memiliki nilai tinggi sebagai perhiasan, sementara tembaga umumnya digunakan dalam industri");
            setMessage.SetTextbox(SetMessage.textboxState.Wrong);
            ekspresiCecep.SetEkspresi(EkspresiCecep.ekspresi.Sedih);
            // nextQuestion.GetComponent<Image>().color = setMessage.boxWrong;

        }
        FindAnyObjectByType<GoalManager>().AddObjectiveCount();
        confirm.gameObject.SetActive(false);
        nextQuestion.gameObject.SetActive(true);
    }
}
