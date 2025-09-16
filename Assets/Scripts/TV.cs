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

        weatherText.text = $"�ϱ⿹��" +
            $"\r\n���� ���� :{value1}" +
            $"\r\n���� ���� : {value2}";
    }
}
