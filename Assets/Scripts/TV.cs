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
        string value1 = WeatherManager.instance.currentWeather.ToString();
        string value2 = WeatherManager.instance.nextWeather.ToString();

        weatherText.text = $"ÀÏ±â¿¹º¸" +
            $"\r\n¿À´Ã ³¯¾¾ :{value1}" +
            $"\r\n³»ÀÏ ³¯¾¾ : {value2}";
    }
}
