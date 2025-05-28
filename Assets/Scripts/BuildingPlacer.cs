using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public BuildingData[] buildingOptions;  
    private GridManager gridManager;
    private int selectedIndex = 0;
    private BuildingData pendingPlacement;
    private GameObject ghostInstance;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
            Debug.LogError("GridManager introuvable dans la scène !");
        
        if (buildingOptions == null || buildingOptions.Length == 0)
            Debug.LogError("Aucun BuildingData assigné dans buildingOptions !");
    }

    void Update()
    {
        if (pendingPlacement != null && ghostInstance != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 worldPoint = hit.point;
                Vector2Int cell = gridManager.GetCellFromWorldPosition(worldPoint);
                Vector3 cellPos = gridManager.GetCellWorldPosition(cell.x, cell.y);
                ghostInstance.transform.position = cellPos;

                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(pendingPlacement.prefab, cellPos, Quaternion.identity);
                    gridManager.OccupyCell(cell);
                    Destroy(ghostInstance);
                    ghostInstance = null;
                    pendingPlacement = null;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    Destroy(ghostInstance);
                    ghostInstance = null;
                    pendingPlacement = null;
                }
            }
        }
    }

    public void StartPlacingBuilding(BuildingData data)
    {
        pendingPlacement = data;
        if (ghostInstance != null) Destroy(ghostInstance);
        ghostInstance = Instantiate(data.prefab);
        foreach (var r in ghostInstance.GetComponentsInChildren<Renderer>())
            r.material.color = new Color(1, 1, 1, 0.5f);
    }
}
