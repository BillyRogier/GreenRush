using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int money = 500;

    private ResourceUI ui;  // référence à ton UI

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        ui = FindObjectOfType<ResourceUI>();
        if (ui == null) Debug.LogError("Pas de ResourceUI trouvé dans la scène !");
        else ui.SetMoney(money);  // affiche la valeur initiale
    }

    public bool Spend(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            ui?.SetMoney(money);    // mise à jour de l'UI
            return true;
        }
        return false;
    }

    public void Add(int amount)
    {
        money += amount;
        ui?.SetMoney(money);        // mise à jour de l'UI
    }
}