using UnityEngine;

public class Plot : MonoBehaviour
{
    public bool isPlanted = false;
    private Plant currentPlant;

    // 씨앗을 심는 함수
    public bool PlantSeed(GameObject plantPrefab, Tile parentTile)
    {
        if (isPlanted)
        {
            Debug.Log("이미 작물이 심어져 있습니다.");
            return false;
        }

        isPlanted = true;
        GameObject plantObject = Instantiate(plantPrefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);
        plantObject.transform.SetParent(transform);
        currentPlant = plantObject.GetComponent<Plant>();
        currentPlant.Initialize(parentTile);

        Debug.Log("씨앗을 심었습니다. 물을 주세요.");
        return true;
    }

    // 물을 주는 함수
    public void Water()
    {
        if (isPlanted && currentPlant != null && currentPlant.currentState == PlantState.Seeded)
        {
            currentPlant.StartGrowing();
            Debug.Log("물을 주어 식물이 자라기 시작합니다.");
        }
    }
}