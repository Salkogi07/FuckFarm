using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherState
{
    Fine = 0,
    Cloud = 1,
    Rain = 2,
    Storm = 3,
    Ice = 4
}

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager instance;
    public GameObject[] weatherEffect;
    public WeatherState currentWeather;
    public Light light;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetRandomWeather();
    }

    public void SetRandomWeather()
    {
        int weatherCount = System.Enum.GetNames(typeof(WeatherState)).Length;
        int randomValue = Random.Range(0, weatherCount);

        currentWeather = (WeatherState)randomValue;

        ApplyWeather();
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
    }

    public void ApplyWeather()
    {
        DeactivateAllEffects();

        switch (currentWeather)
        {
            case WeatherState.Fine:
                UIManager.instance.SetWeatherUI("¸¼À½");
                light.intensity = 1f;
                break;
            case WeatherState.Cloud:
                UIManager.instance.SetWeatherUI("Èå¸²");
                light.intensity = 0.5f;
                RenderSettings.fog = true;
                break;
            case WeatherState.Rain:
                UIManager.instance.SetWeatherUI("ºñ");
                light.intensity = 0.5f;
                weatherEffect[0].SetActive(true);
                break;
            case WeatherState.Storm:
                UIManager.instance.SetWeatherUI("ÆøÇ³");
                light.intensity = 0.5f;
                weatherEffect[1].SetActive(true);
                break;
            case WeatherState.Ice:
                UIManager.instance.SetWeatherUI("¿ì¹Ú");
                light.intensity = 0.5f;
                weatherEffect[2].SetActive(true);
                break;
        }
    }
}
