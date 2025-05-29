using UnityEngine;
using UnityEngine.UI;
using TMPro;      // ← on ajoute TMPro

public class ShopItem : MonoBehaviour
{
    public TextMeshProUGUI     nameText;   // ← on change le type
    public TextMeshProUGUI     priceText;  // ← on change le type
    public TextMeshProUGUI     productionPerMinute;
    public TextMeshProUGUI     incomePerMinute;

    private BuildingData data;

    public void Initialize(BuildingData data)
    {
        this.data = data;
        nameText.text  = data.type.ToString();
        priceText.text = data.cost + " $";
        productionPerMinute.text = data.productionPerMinute + " kWh";
        incomePerMinute.text = data.incomePerMinute + " $";
    }

    public void OnBuyClicked()
    {
        if (MoneyManager.Instance.Spend(data.cost))
        {
            FindObjectOfType<BuildingPlacer>().StartPlacingBuilding(data);
            FindObjectOfType<MenuController>().ShowMainMenu();
        }
        else
        {
            Debug.Log("Pas assez d'argent !");
            // Affiche un message d'erreur à l'écran si tu veux
        }
    }
}
