using UnityEngine;
using UnityEngine.UI;

public class MoneyBar : MonoBehaviour
{
    public Image fillImage;
    public float maxMoney = 100000f;
    public float currentMoney = 0f;

    void Update()
    {
        float ratio = Mathf.Clamp01(currentMoney / maxMoney);
        fillImage.fillAmount = ratio;
    }

    public void SetMoney(float money)
    {
        currentMoney = Mathf.Clamp(money, 0, maxMoney);
    }
}
