using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] Vector3 topRightPosReference;
    public float puzzleSpread;
    Vector3[] correctPosition;
    [SerializeField] float snapDistance = 0.1f;

    GoalManager goalManager;
    int piecesPlaced;
    int totalPieces = 4;

    [Header("Win Panel")]
    [SerializeField] GameObject winPanel;
    [SerializeField] Image background;
    [SerializeField] RectTransform icon, sparkle;

    // Start is called before the first frame update
    void Start()
    {
        goalManager = GetComponent<GoalManager>();
        correctPosition = new Vector3[]{
            new(topRightPosReference.x,topRightPosReference.y,topRightPosReference.z),
            new(-topRightPosReference.x,topRightPosReference.y,topRightPosReference.z),
            new(-topRightPosReference.x,topRightPosReference.y,-topRightPosReference.z),
            new(topRightPosReference.x,topRightPosReference.y,-topRightPosReference.z),
        };
        goalManager.onObjectiveCompleted += GameWin;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckPuzzlePosition(int index, Vector3 position, out Vector3 snapTo)
    {
        if (Vector3.Distance(position, correctPosition[index]) < snapDistance)
        {
            snapTo = correctPosition[index];
            return true;
        }
        else
        {
            snapTo = position;
            return false;
        }
    }

    public void NewPiecesDone()
    {
        piecesPlaced++;
        if (piecesPlaced >= totalPieces) goalManager.AddObjectiveCount();
    }

    void GameWin()
    {
        float duration = 1.5f;
        Color targetColor = background.color;
        background.color = new(targetColor.r, targetColor.g, targetColor.b, 0);
        icon.localScale = sparkle.localScale = Vector3.zero;

        background.rectTransform.LeanAlpha(targetColor.a, duration);
        icon.gameObject.LeanScale(Vector3.one, duration).setDelay(duration).setEase(LeanTweenType.easeOutBounce);
        sparkle.gameObject.LeanScale(Vector3.one, duration).setDelay(duration).setEase(LeanTweenType.easeOutBounce);
        sparkle.LeanRotateAround(Vector3.forward, 360, 10).setLoopClamp();
        Invoke(nameof(AfterWin), 5);
    }

    void AfterWin()
    {
        winPanel.SetActive(false);
        goalManager.gameObject.GetComponent<DialogManager>().NextDialog();
    }
}
