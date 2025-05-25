using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "CityBuilder/Building")]
public class BuildingData : ScriptableObject
{
    public BuildingType type;
    public GameObject prefab;
    public int cost;
    public float productionPerMinute;
    public float incomePerMinute;
}
