using System;
using UnityEngine;

public enum SeedType { ���, ����ݸ�, �عٶ��, �ݸ��ö��, ������ }
public enum GrowthStage { Planted, Growing, Mature }

[CreateAssetMenu(fileName = "cropData", menuName ="�۹�")]
public class CropData : ScriptableObject
{
    public SeedType seedType;
    public WeatherState[] weatherState;
    public float maxPlantTime;

    public float minGrowTime, maxGrowTime;
    public float minHumidity, maxHumidity;
    public float minPrice, maxPrice;

    public GameObject seedModels;
    public GameObject growingModels;
    public GameObject matureModels;
}
