using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Toutes tes données de bâtiments (attachées aux prefabs)")]
    public List<BuildingData> buildingDatas;

    [Header("Prefab d’affichage d’un item de shop")]
    public GameObject shopItemPrefab;
    public Transform contentParent;

    void Start()
{
    Debug.Log($"[Shop] buildingDatas.Count = {buildingDatas?.Count}");
    if (buildingDatas == null || shopItemPrefab == null || contentParent == null)
    {
        Debug.LogError("[Shop] UN CHAMP N’EST PAS ASSIGNÉ !");
        return;
    }
    foreach(var data in buildingDatas)
    {
        Debug.Log($"[Shop] Instantiating item for {data.type}");
                var go = Instantiate(shopItemPrefab, contentParent);
            var item = go.GetComponent<ShopItem>();
            item.Initialize(data);
    }
}

}
