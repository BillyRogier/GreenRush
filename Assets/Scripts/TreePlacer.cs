using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public GameObject[] treePrefabs; // Liste de prefabs d'arbres à placer
    public int numberOfTrees = 50;

    public GridManager gridManager; // Référence vers ton GridManager

    void Start()
    {
        PlaceTrees();
    }

    void PlaceTrees()
    {
        int tries = 0;
        int treesPlaced = 0;

        while (treesPlaced < numberOfTrees && tries < numberOfTrees * 10)
        {
            tries++;

            int x = Random.Range(0, gridManager.width);
            int y = Random.Range(0, gridManager.height);

            Vector2Int cell = new Vector2Int(x, y);

            if (!gridManager.IsCellFree(gridManager.GetCellWorldPosition(x, y), out _))
                continue; // Cellule occupée, on skip

            Vector3 pos = gridManager.GetCellWorldPosition(x, y);

            GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
            Instantiate(treePrefab, pos, Quaternion.identity, transform);

            treesPlaced++;

            // Marque la cellule comme occupée (facultatif si tu ne veux pas que d'autres objets s'y placent)
            gridManager.OccupyCell(cell);
        }

        Debug.Log($"{treesPlaced} arbres placés aléatoirement.");
    }
}
