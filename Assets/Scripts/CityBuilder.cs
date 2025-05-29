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

    // Taille dynamique de la ville
    private int citySizeX = 8;
    private int citySizeY = 8;

    void Start()
    {
        InitPrefabMap();
        InitDataMap();
        PlaceInitialBuildings();
    }

    void Update()
    {
        // Shift + 3 (KeyCode.Alpha3 avec Shift)
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            ExpandCity();
        }
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

    void ExpandCity()
    {
        // On augmente la taille, sans dépasser la taille de la grille
        citySizeX = Mathf.Min(citySizeX + 1, gridManager.width);
        citySizeY = Mathf.Min(citySizeY + 1, gridManager.height);

        // Optionnel : détruire les anciens bâtiments avant de replacer
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        PlaceInitialBuildings();
    }

    void PlaceInitialBuildings()
    {
        int sizeX = Mathf.Min(citySizeX, gridManager.width);
        int sizeY = Mathf.Min(citySizeY, gridManager.height);

        // Calcul du décalage pour centrer la ville
        int offsetX = (gridManager.width - sizeX) / 2;
        int offsetY = (gridManager.height - sizeY) / 2;

        int roadPrefabIndex = 0; // Le prefab d'index 0 doit être une rue
        int buildingStartIndex = 1; // Les autres sont des bâtiments

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                int gridX = x + offsetX;
                int gridY = y + offsetY;

                // Rues horizontales et verticales tous les 3 blocs
                if (x % 3 == 0 || y % 3 == 0)
                {
                    PlaceBuilding(roadPrefabIndex, gridX, gridY);
                }
                else
                {
                    // Place un bâtiment aléatoire (hors prefab 0)
                    int prefabIndex = Random.Range(buildingStartIndex, buildingPrefabs.Length);
                    PlaceBuilding(prefabIndex, gridX, gridY);
                }
            }
        }
    }

    void PlaceBuilding(int prefabIndex, int x, int y)
    {
        if (prefabIndex < 0 || prefabIndex >= buildingPrefabs.Length) return;

        Vector3 worldPos = gridManager.GetCellWorldPosition(x, y);

        // Si c'est une route (index 0), on place à y = 0.02
        if (prefabIndex == 0)
            worldPos.y = 0.02f;

        GameObject building = Instantiate(buildingPrefabs[prefabIndex], worldPos, Quaternion.identity, transform);
        gridManager.OccupyCell(new Vector2Int(x, y));
    }

    public void PlaceBuilding(BuildingType type, int x, int y)
    {
        if (!prefabMap.ContainsKey(type)) return;

        Vector3 worldPos = gridManager.GetCellWorldPosition(x, y);
        GameObject building = Instantiate(prefabMap[type], worldPos, Quaternion.identity, transform);
        gridManager.OccupyCell(new Vector2Int(x, y));
    }
}