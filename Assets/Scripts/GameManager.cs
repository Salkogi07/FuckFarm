using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float health = 0;
    public float maxHealth = 100;
    public float money = 3000;

    [Header("Time")]
    public float gameDayTime = 120f;
    public float currentTime = 0;
    public int day = 0;
    public int hour = 0;
    public int minute = 0;

    public bool IsTimeUp = false;
    public bool IsPause = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = 0;
    }

    private void Update()
    {
        GameTime();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            IsPause = !IsPause;
            Time.timeScale = IsPause ? 0 : 1;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            money += 10000;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            IsTimeUp = !IsTimeUp;
            Time.timeScale = IsTimeUp ? 2 : 1;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {

        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            WeatherManager.instance.SetNextWeather();
        }
    }

    private void GameTime()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= gameDayTime)
        {
            currentTime -= gameDayTime;
            day++;
            WeatherManager.instance.SetRandomWeather();
        }

        float timeRatio = currentTime / gameDayTime;

        hour = (int)(timeRatio * 24);
        minute = (int)(((timeRatio * 24) - hour) * 60);
    }
}
