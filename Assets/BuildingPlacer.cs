using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;
    private GridManager gridManager;

    void Start()
    {
        gridManager = Object.FindFirstObjectByType<GridManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 point = hit.point;

                if (gridManager.IsCellFree(point, out Vector2Int cell))
                {
                    Vector3 cellPos = new Vector3(cell.x * gridManager.cellSize, 0, cell.y * gridManager.cellSize);
                    Instantiate(buildingPrefab, cellPos, Quaternion.identity);
                    gridManager.OccupyCell(cell);
                }
                else
                {
                    Debug.Log("Case déjà occupée !");
                }
            }
        }
    }
}
