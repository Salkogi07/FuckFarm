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

        weatherText.text = $"�ϱ⿹��" +
            $"\r\n���� ���� :{value1}" +
            $"\r\n���� ���� : {value2}";
    }

    private string KrWeather(WeatherState value)
    {
        switch (value)
        {
            case WeatherState.Fine:
                return "����";
            case WeatherState.Rain:
                return "��";
            case WeatherState.Cloud:
                return "�帲";
            case WeatherState.Storm:
                return "��ǳ";
            case WeatherState.Ice:
                return "���";
            default:
                return "";
        }
    }
}
