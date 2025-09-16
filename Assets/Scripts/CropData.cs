using System;
using UnityEngine;

public enum SeedType { 당근, 브로콜리, 해바라기, 콜리플라워, 옥수수 }
public enum GrowthStage { Planted, Growing, Mature }

[CreateAssetMenu(fileName = "cropData", menuName ="작물")]
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
