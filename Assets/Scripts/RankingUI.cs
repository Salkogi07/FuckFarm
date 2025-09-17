using System;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    public static RankingUI instance;

    public GameObject panel;
    public Text[] rankTexts;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        RefreshUI();
    }

    public void RefreshUI()
    {
        var ranks = RankingManager.Instance.GetRanking();

        for (int i = 0; i < rankTexts.Length; i++)
        {
            if (i < ranks.Count)
            {
                float value = ranks[i].score;
                int minutes = Mathf.FloorToInt(value / 60f);
                int seconds = Mathf.FloorToInt(value % 60f);

                string text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
                rankTexts[i].text = $"{i + 1}À§ : {ranks[i].name} - {text}ÃÊ";
            }
            else
            {
                rankTexts[i].text = $"{i + 1}À§ : ---";
            }
        }
    }
}
