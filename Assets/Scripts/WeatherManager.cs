using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherState
{
    ¸¼À½ = 0,
    Èå¸² = 1,
    ºñ = 2,
    ÆøÇ³ = 3,
    ¿ì¹Ú = 4
}

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager instance;
    public GameObject[] weatherEffect;
    public WeatherState currentWeather;
    public WeatherState nextWeather;
    public Light light;
    public Terrain terrain;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentWeather = SetRandomWeather();
        ApplyWeather();

        nextWeather = SetRandomWeather();
    }

    public WeatherState SetRandomWeather()
    {
        int weatherCount = System.Enum.GetNames(typeof(WeatherState)).Length;
        int randomValue = Random.Range(0, weatherCount);

        WeatherState value = (WeatherState)randomValue;

        return value;
    }

    public void SetNextWeather()
    {
        int weatherCount = System.Enum.GetNames(typeof(WeatherState)).Length;
        int nextStateIndex = ((int)currentWeather + 1) % weatherCount;
        currentWeather = (WeatherState)nextStateIndex;
        ApplyWeather();
    }

    private void DeactivateAllEffects()
    {
        foreach (GameObject effect in weatherEffect)
        {
            effect.SetActive(false);
        }
        RenderSettings.fog = false;
        terrain.terrainData.wavingGrassStrength = .5f;
        terrain.terrainData.wavingGrassAmount = .5f;
        terrain.terrainData.wavingGrassSpeed = .5f;
    }

    public void ApplyWeather()
    {
        DeactivateAllEffects();

        switch (currentWeather)
        {
            case WeatherState.¸¼À½:
                light.intensity = 1f;
                break;
            case WeatherState.Èå¸²:
                light.intensity = 0.5f;
                RenderSettings.fog = true;
                break;
            case WeatherState.ºñ:
                light.intensity = 0.5f;
                weatherEffect[0].SetActive(true);
                break;
            case WeatherState.ÆøÇ³:
                light.intensity = 0.5f;
                terrain.terrainData.wavingGrassStrength = 1f;
                terrain.terrainData.wavingGrassAmount = 1f;
                terrain.terrainData.wavingGrassSpeed = 1f;
                weatherEffect[1].SetActive(true);
                break;
            case WeatherState.¿ì¹Ú:
                light.intensity = 0.5f;
                weatherEffect[2].SetActive(true);
                break;
        }
        UIManager.instance.SetWeatherUI(currentWeather.ToString());
    }
}
