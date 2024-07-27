using UnityEngine;
using UnityEngine.UI;

public class EkspresiCecep : MonoBehaviour
{
    public static EkspresiCecep instance { get; private set; }
    [SerializeField] Sprite netral, sedih, senang;
    public enum ekspresi
    {
        Netral,
        Sedih,
        Senang
    }
    Image container;

    void Awake()
    {
        instance = this;
        container = GetComponent<Image>();
    }

    void Start()
    {
    }

    public void SetEkspresi(ekspresi ekspresi)
    {
        switch (ekspresi)
        {
            case ekspresi.Netral:
                container.sprite = netral;
                break;
            case ekspresi.Sedih:
                container.sprite = sedih;
                Invoke(nameof(ResetExpression), 3);
                break;
            case ekspresi.Senang:
                container.sprite = senang;
                break;
            default:
                container.sprite = netral;
                break;
        }
    }

    void ResetExpression() => container.sprite = netral;
}

