using UnityEngine;
using UnityEngine.UI;
using TMPro;      // ← on ajoute TMPro

public class ShopItem : MonoBehaviour
{
    public Image               icon;
    public TextMeshProUGUI     nameText;   // ← on change le type
    public TextMeshProUGUI     priceText;  // ← on change le type

    private BuildingData data;

    public void Initialize(BuildingData data)
    {
        this.data = data;
        nameText.text  = data.type.ToString();
        priceText.text = data.cost + " $";
    }

    public void OnBuyClicked()
    {
        Debug.Log($"Tu achètes : {data.type} pour {data.cost}");
        // ta logique d'achat ici…
    }
}
