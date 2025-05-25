using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public GameObject cellPrefab;

    private bool[,] occupied;
    private Vector3[,] grid;

    void Awake()
    {
        grid = new Vector3[width, height];
        occupied = new bool[width, height];
        CreateGrid();
    }

    public void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(
                    x * cellSize - (width * cellSize / 2f) + cellSize / 2f,
                    0,
                    y * cellSize - (height * cellSize / 2f) + cellSize / 2f
                );
                grid[x, y] = pos;
                Instantiate(cellPrefab, pos, Quaternion.identity);
            }
        }
    }

    public Vector2Int GetCellFromWorldPosition(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos.x + (width * cellSize / 2f)) / cellSize);
        int y = Mathf.FloorToInt((worldPos.z + (height * cellSize / 2f)) / cellSize);
        return new Vector2Int(x, y);
    }

    public bool IsCellFree(Vector3 worldPos, out Vector2Int cell)
    {
        cell = GetCellFromWorldPosition(worldPos);

        if (cell.x < 0 || cell.x >= width || cell.y < 0 || cell.y >= height)
            return false;

        return !occupied[cell.x, cell.y];
    }

    public void OccupyCell(Vector2Int cell)
    {
        if (cell.x < 0 || cell.x >= width || cell.y < 0 || cell.y >= height)
            return;

        occupied[cell.x, cell.y] = true;
    }

    public Vector3 GetCellWorldPosition(int x, int y)
    {
        return grid[x, y];
    }
}
