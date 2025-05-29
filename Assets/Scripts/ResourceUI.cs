using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI productionText;

    void Start()
    {
        if (MoneyManager.Instance != null)
            SetMoney(MoneyManager.Instance.money);
        else
            Debug.LogError("Pas de MoneyManager trouvé au Start de ResourceUI");

        SetProduction(0f);
    }

    public void SetMoney(int value)
    {
        moneyText.text = $"{value} $";
    }

    public void SetProduction(float value)
    {
        productionText.text = $"{value:F0} kWh";
    }
}