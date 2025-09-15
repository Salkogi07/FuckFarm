using UnityEngine;

public enum ItemType
{
    None,
    Seed,
    Tool
}

// 밭의 상태를 정의합니다.
public enum PlantState
{
    Empty,
    Seeded,
    Growing,
    Harvestable
}

public class Plant : MonoBehaviour
{
    public PlantState currentState = PlantState.Seeded;
    private Tile parentTile;
    private float growthProgress = 0f;
    private float growthGoal = 100f;

    public void Initialize(Tile tile)
    {
        parentTile = tile;
        transform.localScale = Vector3.one * 0.2f;
    }

    void Update()
    {
        if (currentState == PlantState.Growing)
        {
            // 타일의 습도가 30~80 사이일 때만 성장
            if (parentTile.humidity > 30f && parentTile.humidity < 80f)
            {
                growthProgress += 10f * Time.deltaTime;
                transform.localScale = Vector3.one * (0.2f + (growthProgress / growthGoal) * 0.8f);

                if (growthProgress >= growthGoal)
                {
                    currentState = PlantState.Harvestable;
                    GetComponent<Renderer>().material.color = Color.yellow;
                    Debug.Log("식물 수확 준비 완료!");
                }
            }
        }
    }

    public void StartGrowing()
    {
        currentState = PlantState.Growing;
    }
}