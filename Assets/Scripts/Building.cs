using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData data;

    public void Initialize(BuildingData d)
    {
        data = d;
    }
}