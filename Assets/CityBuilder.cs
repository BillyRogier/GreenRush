using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject[] buildingPrefabs;

    void Start()
    {
        PlaceInitialBuildings();
    }

    void PlaceInitialBuildings()
    {
        // Bloc central
        PlaceBuilding(0, 4, 4);
        PlaceBuilding(1, 5, 4);
        PlaceBuilding(2, 4, 5);
        PlaceBuilding(3, 5, 5);

        // Une rangée vers le haut
        PlaceBuilding(4, 4, 6);
        PlaceBuilding(5, 5, 6);

        // Une rangée vers le bas
        PlaceBuilding(1, 4, 3);
        PlaceBuilding(2, 5, 3);

        // Une colonne vers la gauche
        PlaceBuilding(0, 3, 4);
        PlaceBuilding(1, 3, 5);

        // Une colonne vers la droite
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
}
