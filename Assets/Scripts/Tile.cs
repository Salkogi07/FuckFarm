using UnityEngine;

public class Tile : MonoBehaviour
{
    public float humidity = 50f;
    public Plot[] plots;

    void Awake()
    {
        plots = GetComponentsInChildren<Plot>();
    }

    void Update()
    {
        humidity -= 0.2f * Time.deltaTime;
        if (humidity < 0) humidity = 0;
    }

    public void AddHumidity(float amount)
    {
        humidity += amount;
        if (humidity > 100) humidity = 100;
        Debug.Log($"타일의 현재 습도: {humidity:F1}");
    }
}