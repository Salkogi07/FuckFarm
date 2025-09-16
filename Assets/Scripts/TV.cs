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

        weatherText.text = "�ϱ⿹��" +
            $"\r\n���� ���� :{value1}" +
            $"\r\n���� ���� : {value2}";

       GameManager game = GameManager.instance;

        moneyText.text = "�۹����� ����" +
            $"\r\n ��� : {game.price[0]} ({game.price[0] - game.lastPrice[0]})" +
            $"\r\n ������ : {game.price[1]} ({game.price[1] - game.lastPrice[1]})" +
            $"\r\n ����ݸ� : {game.price[2]} ({game.price[2] - game.lastPrice[2]})" +
            $"\r\n �عٶ�� : {game.price[3]} ({game.price[3] - game.lastPrice[3]})" +
            $"\r\n �ݸ��ö�� : {game.price[4]} ({game.price[4] - game.lastPrice[4]})";
    }
}
