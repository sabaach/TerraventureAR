using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class PuzzleScript : MonoBehaviour
{
    Draggable3D draggable;
    PuzzleManager puzzleManager;
    // Plane plane = new Plane(Vector3.up, 0);

    void OnEnable()
    {
        if (draggable == null) draggable = GetComponent<Draggable3D>();
        if (puzzleManager == null) puzzleManager = FindAnyObjectByType<PuzzleManager>();

        draggable.onDragRelease += CheckPuzzle;
        float spread = puzzleManager.puzzleSpread;
        transform.localPosition = new(
            Random.Range(-spread, spread),
            0,
            Random.Range(-spread, spread)
        );
    }

    void OnDisable()
    {
        draggable.onDragRelease -= CheckPuzzle;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckPuzzle()
    {
        if (puzzleManager.CheckPuzzlePosition(transform.GetSiblingIndex(), transform.localPosition, out Vector3 snapTo))
        {
            transform.localPosition = new(snapTo.x, transform.localPosition.y, snapTo.z);
            draggable.enabled = false;
            gameObject.LeanColor(Color.green, 0.5f).setOnComplete(() =>
            {
                gameObject.LeanColor(Color.white, 0.5f).setOnComplete(() => { puzzleManager.NewPiecesDone(); });
            });
        }
    }
}
