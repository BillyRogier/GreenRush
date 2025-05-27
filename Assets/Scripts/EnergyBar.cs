using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image greenBar;
    public Image fossilBar;

    [Range(0, 1)] public float greenRatio = 0.5f;

    void Update()
    {
        greenBar.fillAmount = greenRatio;
        fossilBar.fillAmount = 1f - greenRatio;
    }

    // Appelle cette fonction pour mettre à jour la jauge
    public void SetEnergyRatio(float green)
    {
        greenRatio = Mathf.Clamp01(green);
    }
}
