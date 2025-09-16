using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    [Header("밭 상태")]
    public float growPower = 1;
    public bool isPlowed = false;
    public bool hasPlant = false;
    public CropData cropData;
    public GrowthStage stage = GrowthStage.Planted;
    public float plantTime = 0f;
    public GameObject plantModel;
    public Transform plantPos;

    public MeshFilter plantFilter;
    public Mesh[] mesh;

    [Header("UI")]
    public Image plantImage;

    // 밭 갈기
    public bool Plow(GameObject obj, bool up)
    {
        if (isPlowed && stage != GrowthStage.Mature) return false;

        if (hasPlant && stage == GrowthStage.Mature)
        {
            Item newItem = new Item(cropData, false);

            int value = 1;
            if (up)
                value = Random.Range(1, 4);

            for (int i = 0; i < value; i++)
                obj.GetComponent<CropInventory>().AddItem(newItem);

            DestoryCrop();
            return true;
        }
        else
        {
            isPlowed = true;
            plantFilter.mesh = mesh[1];
            UIManager.instance.ShowMsg("밭 갈기");

            return true;
        }
    }

    // 씨앗 심기
    public bool PlantSeed(CropData cropData)
    {
        if (!isPlowed || hasPlant) return false;

        hasPlant = true;
        this.cropData = cropData;

        // 모델 생성
        plantModel = Instantiate(cropData.seedModels, plantPos.position, Quaternion.identity);
        UIManager.instance.ShowMsg($"{cropData.seedType} 심기");
        return true;
    }

    public void DestoryCrop()
    {
        cropData = null;
        hasPlant = false;
        isPlowed = false;
        plantFilter.mesh = mesh[0];
        stage = GrowthStage.Planted;
        plantTime = 0;
        Destroy(plantModel);
    }

    void Update()
    {
        if(cropData != null)
            plantImage.fillAmount = plantTime / cropData.maxPlantTime;
        else
            plantImage.fillAmount = 0;

        if (hasPlant && stage != GrowthStage.Mature)
        {
            float halfGrowthTime = cropData.maxPlantTime / 2f;
            float fullGrowthTime = cropData.maxPlantTime;

            // 성장 조건 체크 (시간대, 날씨)
            bool canGrow = IsGoodGrowingCondition(cropData);

            if (canGrow)
            {
                plantTime += Time.deltaTime * growPower;
                plantTime = Mathf.Clamp(plantTime, 0, fullGrowthTime);

                // 중간단계
                if (stage == GrowthStage.Planted && plantTime >= halfGrowthTime)
                {
                    Destroy(plantModel);
                    plantModel = Instantiate(cropData.growingModels, plantPos.position, Quaternion.identity);
                    stage = GrowthStage.Growing;
                }
                // 다자람
                else if (stage == GrowthStage.Growing && plantTime >= fullGrowthTime)
                {
                    Destroy(plantModel);
                    plantModel = Instantiate(cropData.matureModels, plantPos.position, Quaternion.identity);
                    stage = GrowthStage.Mature;
                }
            }
        }
    }

    bool IsGoodGrowingCondition(CropData cropData)
    {
        FarmTile tile = GetComponentInParent<FarmTile>();
        int currentHour = GameManager.instance.hour;
        bool goodTime = false;
        bool goodHumidity = false;

        if (cropData.minGrowTime <= cropData.maxGrowTime)
        {
            goodTime = currentHour >= cropData.minGrowTime && currentHour <= cropData.maxGrowTime;
        }
        else
        {
            goodTime = currentHour >= cropData.minGrowTime || currentHour <= cropData.maxGrowTime;
        }

        if(tile.humidity >= cropData.minHumidity && tile.humidity <= cropData.maxHumidity)
        {
            goodHumidity = true;
        }

        WeatherState currentWeather = WeatherManager.instance.currentWeather;
        bool goodWeather = System.Array.Exists(cropData.weatherState, w => w == currentWeather);

        return goodTime && goodWeather && goodHumidity;
    }
}