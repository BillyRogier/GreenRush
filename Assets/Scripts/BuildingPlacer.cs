using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    // Remplace buildingPrefabs[] par un array de BuildingData (ScriptableObject)
    public BuildingData[] buildingOptions;  
    private GridManager gridManager;
    private int selectedIndex = 0;

    void Start()
    {
        // Récupère le GridManager dans la scène
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
            Debug.LogError("GridManager introuvable dans la scène !");
        
        // Vérifie qu'il y a bien au moins un BuildingData d'assigné
        if (buildingOptions == null || buildingOptions.Length == 0)
            Debug.LogError("Aucun BuildingData assigné dans buildingOptions !");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            selectedIndex = 0;
            Debug.Log($"Type de bâtiment sélectionné : {buildingOptions[selectedIndex].type}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (buildingOptions.Length > 1)
            {
                selectedIndex = 1;
                Debug.Log($"Type de bâtiment sélectionné : {buildingOptions[selectedIndex].type}");
            }
            else
            {
                Debug.LogWarning("Pas de deuxième BuildingData dans buildingOptions.");
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 worldPoint = hit.point;

                if (gridManager.IsCellFree(worldPoint, out Vector2Int cell))
                {
                    if (selectedIndex < 0 || selectedIndex >= buildingOptions.Length)
                    {
                        Debug.LogError($"Index {selectedIndex} hors-bornes pour buildingOptions (taille = {buildingOptions.Length})");
                        return;
                    }

                    BuildingData chosenData = buildingOptions[selectedIndex];
                    Vector3 cellPos = gridManager.GetCellWorldPosition(cell.x, cell.y);

                    GameObject instance = Instantiate(chosenData.prefab, cellPos, Quaternion.identity);
                    var bi = instance.GetComponent<BuildingInstance>();
                    if (bi != null)
                    {
                        bi.Init(chosenData);
                    }
                    else
                    {
                        Debug.LogWarning("Le prefab n'a pas de BuildingInstance : impossible d'initialiser les données.");
                    }

                    gridManager.OccupyCell(cell);
                    Debug.Log($"Bâtiment '{chosenData.type}' placé en [{cell.x},{cell.y}]");
                }
                else
                {
                    Debug.Log("Case déjà occupée ou en-dehors de la grille !");
                }
            }
            else
            {
                Debug.Log("Raycast n'a touché aucun collider.");
            }
        }
    }
}
