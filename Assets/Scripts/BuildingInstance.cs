using UnityEngine;

public class BuildingInstance : MonoBehaviour
{
    private BuildingData data;

    public void Init(BuildingData buildingData)
    {
        data = buildingData;
    }

    public float GetProductionPerMinute()
    {
        return (data != null ? data.productionPerMinute : 0f);
    }

    public float GetIncomePerMinute()
    {
        return (data != null ? data.incomePerMinute : 0f);
    }

    public int GetCost()
    {
        return (data != null ? data.cost : 0);
    }

    public BuildingType GetBuildingType()
    {
        return (data != null ? data.type : BuildingType.Eolienne);
    }
}
