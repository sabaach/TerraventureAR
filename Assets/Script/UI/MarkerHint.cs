using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerHint : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Color backgroundShow, backgroundHide;
    [SerializeField] RectTransform icon;
    Vector2 closedPos, shownPos;
    Button iconButton;
    bool hintShown;
    float duration;

    // Start is called before the first frame update
    void Start()
    {
        iconButton = icon.GetComponent<Button>();
        shownPos = new(-180, -342);
        closedPos = new(-56, -57);
        icon.transform.localScale = Vector3.zero;
        icon.anchoredPosition = shownPos;
        LeanTween.scale(icon, Vector3.one, 1).setEase(LeanTweenType.easeInOutQuad);
        Invoke(nameof(CloseHint), 2);
    }

    public void SwitchHintState()
    {
        if (hintShown) CloseHint();
        else OpenHint();
    }

    public void CloseHint()
    {
        iconButton.interactable = false;
        background.raycastTarget = false;
        float duration = 0.5f;
        // background.color = new Color(0, 0, 0, 0.85f);
        // background.CrossFadeColor(new Color(0, 0, 0, 0), 1, true, true);
        // background.gameObject.LeanAlpha(0, 1);
        background.gameObject.LeanValue(backgroundShow, backgroundHide, duration)
            .setOnUpdate((Color val) =>
            {
                background.color = val;
            });

        icon.gameObject.LeanValue(0, 1, duration).setEase(LeanTweenType.easeOutQuad).setOnUpdate((float value) =>
        {
            icon.anchoredPosition = Vector2.Lerp(shownPos, closedPos, value);
        });
        // icon.LeanMoveLocal(new Vector2(126.84f, 262.2f), 1).setEase(LeanTweenType.easeOutQuad);
        icon.LeanScale(Vector3.one * 0.423f, duration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => { iconButton.interactable = true; });
        hintShown = false;
    }

    void OpenHint()
    {
        float duration = 0.6f;
        iconButton.interactable = false;
        background.raycastTarget = true;
        background.gameObject.LeanValue(backgroundHide, backgroundShow, duration)
            .setOnUpdate((Color val) =>
            {
                background.color = val;
            });
        icon.gameObject.LeanValue(1, 0, duration).setEase(LeanTweenType.easeOutQuad).setOnUpdate((float value) =>
        {
            icon.anchoredPosition = Vector2.Lerp(shownPos, closedPos, value);
        });
        icon.transform.LeanScale(Vector3.one, duration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => { iconButton.interactable = true; });
        hintShown = true;
    }
}
