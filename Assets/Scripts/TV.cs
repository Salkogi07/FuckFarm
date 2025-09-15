using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
    public Text weatherText;
    public Text moneyText;

    private void Update()
    {
        string value1 = KrWeather(WeatherManager.instance.currentWeather);
        string value2 = KrWeather(WeatherManager.instance.nextWeather);

        weatherText.text = $"ÀÏ±â¿¹º¸" +
            $"\r\n¿À´Ã ³¯¾¾ :{value1}" +
            $"\r\n³»ÀÏ ³¯¾¾ : {value2}";
    }

    private string KrWeather(WeatherState value)
    {
        switch (value)
        {
            case WeatherState.Fine:
                return "¸¼À½";
            case WeatherState.Rain:
                return "ºñ";
            case WeatherState.Cloud:
                return "Èå¸²";
            case WeatherState.Storm:
                return "ÆøÇ³";
            case WeatherState.Ice:
                return "¿ì¹Ú";
            default:
                return "";
        }
    }
}
