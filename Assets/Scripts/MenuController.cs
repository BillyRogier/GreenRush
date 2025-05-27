using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject shopPanel;

    // Appelé par le bouton Shop
    public void ShowShop()
    {
        mainMenuPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    // Appelé par un futur bouton "Back" dans le shop
    public void ShowMainMenu()
    {
        shopPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
