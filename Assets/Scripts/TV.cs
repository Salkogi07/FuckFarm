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

        weatherText.text = "일기예보" +
            $"\r\n오늘 날씨 :{value1}" +
            $"\r\n내일 날씨 : {value2}";

       GameManager game = GameManager.instance;

        moneyText.text = "작물가격 변동" +
            $"\r\n 당근 : {game.price[0]} ({game.price[0] - game.lastPrice[0]})" +
            $"\r\n 옥수수 : {game.price[1]} ({game.price[1] - game.lastPrice[1]})" +
            $"\r\n 브로콜리 : {game.price[2]} ({game.price[2] - game.lastPrice[2]})" +
            $"\r\n 해바라기 : {game.price[3]} ({game.price[3] - game.lastPrice[3]})" +
            $"\r\n 콜리플라워 : {game.price[4]} ({game.price[4] - game.lastPrice[4]})";
    }
}
