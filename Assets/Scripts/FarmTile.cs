using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmTile : MonoBehaviour
{
    [Header("≈∏¿œ ªÛ≈¬")]
    public float humidity = 50;

    [Header("πÁµÈ")]
    public Farm[] farms = new Farm[9];

    [Header("UI")]
    public Image image;

    void Start()
    {
        farms = GetComponentsInChildren<Farm>();
    }

    public void WaterTile(float value)
    {
        humidity += value;
        humidity = Mathf.Clamp(humidity, 0, 100);
    }

    void Update()
    {
        image.fillAmount = humidity / 100;

        if (Time.time % 5f < Time.deltaTime)
        {
            switch (WeatherManager.instance.currentWeather)
            {
                case WeatherState.∏º¿Ω:
                    humidity = Mathf.Clamp(humidity - 1, 0, 100);
                    UpdateGrowPower(2);
                    break;
                case WeatherState.»Â∏≤:
                    humidity = Mathf.Clamp(humidity - 1, 0, 100);
                    UpdateGrowPower(1);
                    break;
                case WeatherState.∫Ò:
                    humidity = Mathf.Clamp(humidity + 1, 0, 100);
                    UpdateGrowPower(2);
                    break;
                case WeatherState.∆¯«≥:
                    humidity = Mathf.Clamp(humidity - 10, 0, 100);
                    UpdateGrowPower(-1);
                    break;
                case WeatherState.øÏπ⁄:
                    humidity = Mathf.Clamp(humidity - 15, 0, 100);
                    UpdateGrowPower(1);
                    RandomDestoryCrop();
                    break;
            }
        }
    }

    private void UpdateGrowPower(float growPower)
    {
        foreach(var farm in farms)
        {
            farm.growPower = growPower;
        }
    }

    private void RandomDestoryCrop()
    {
        int value = Random.Range(0, 100);

        if(value < 2)
        {
            List<Farm> plantedFarms = new List<Farm>();

            foreach (var farm in farms)
            {
                if(farm.hasPlant)
                {
                    plantedFarms.Add(farm);
                }
            }

            int destroyedCount = 0;
            
            while (destroyedCount < 2 && plantedFarms.Count > 0)
            {
                int randomIndex = Random.Range(0, plantedFarms.Count);
                Farm farmToDestroy = plantedFarms[randomIndex];

                farmToDestroy.DestoryCrop();

                plantedFarms.RemoveAt(randomIndex);
                destroyedCount++;
            }
        }
    }
}