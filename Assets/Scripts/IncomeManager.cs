using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    public float interval = 5f;
    private float timer;
    private ResourceUI ui;
    private float totalProduction;

    void Start()
    {
        ui = FindObjectOfType<ResourceUI>();
        if (ui == null)
        {
            Debug.LogError("IncomeManager : pas de ResourceUI dans la scène !");
        }
        else
        {
            ui.SetProduction(totalProduction);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            GiveIncome();
        }
    }

    void GiveIncome()
    {
        float income = 0f;
        float production = 0f;
        foreach (var b in FindObjectsOfType<Building>())
        {
            income     += b.data.incomePerMinute     * (interval / 60f);
            production += b.data.productionPerMinute * (interval / 60f);
        }

        MoneyManager.Instance.Add(Mathf.RoundToInt(income));
        totalProduction += production;

        if (ui != null)
            ui.SetProduction(totalProduction);
    }
}