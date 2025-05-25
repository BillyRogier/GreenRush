using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject[] buildingPrefabs;

    [System.Serializable]
    public class BuildingDefinition
    {
        public BuildingType type;
        public GameObject prefab;
    }

    public BuildingDefinition[] buildingDefinitions;
    private Dictionary<BuildingType, GameObject> prefabMap;
    public BuildingData[] buildingsData;
    private Dictionary<BuildingType, BuildingData> dataMap;

    void Start()
    {
        InitPrefabMap();
        InitDataMap();
        PlaceInitialBuildings();
    }

    void InitPrefabMap()
    {
        prefabMap = new Dictionary<BuildingType, GameObject>();
        foreach (var def in buildingDefinitions)
        {
            prefabMap[def.type] = def.prefab;
        }
    }

    void InitDataMap()
    {
        dataMap = new Dictionary<BuildingType, BuildingData>();
        foreach (var data in buildingsData)
        {
            dataMap[data.type] = data;
        }
    }

    void PlaceInitialBuildings()
    {
        PlaceBuilding(0, 4, 4);
        PlaceBuilding(1, 5, 4);
        PlaceBuilding(2, 4, 5);
        PlaceBuilding(3, 5, 5);

        PlaceBuilding(4, 4, 6);
        PlaceBuilding(5, 5, 6);

        PlaceBuilding(1, 4, 3);
        PlaceBuilding(2, 5, 3);

        PlaceBuilding(0, 3, 4);
        PlaceBuilding(1, 3, 5);

        PlaceBuilding(0, 6, 4);
        PlaceBuilding(2, 6, 5);
    }

    void PlaceBuilding(int prefabIndex, int x, int y)
    {
        if (prefabIndex < 0 || prefabIndex >= buildingPrefabs.Length) return;

        Vector3 worldPos = gridManager.GetCellWorldPosition(x, y);
        GameObject building = Instantiate(buildingPrefabs[prefabIndex], worldPos, Quaternion.identity);
        gridManager.OccupyCell(new Vector2Int(x, y));
    }

    public void PlaceBuilding(BuildingType type, int x, int y)
    {
        if (!prefabMap.ContainsKey(type)) return;

        Vector3 worldPos = gridManager.GetCellWorldPosition(x, y);
        GameObject building = Instantiate(prefabMap[type], worldPos, Quaternion.identity);
        gridManager.OccupyCell(new Vector2Int(x, y));
    }
}
