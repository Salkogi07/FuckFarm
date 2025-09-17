using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject msgBoxUI, tileBuyUI, pauseUI;
    public Text msgText, tileBuyText;

    public Text timeText, dayText, weatherText, moneyText, healthText;
    public Image moneyImage, healthImage;

    [Header("Rank")]
    public GameObject rankSavePanel;
    public InputField field;
    public Text score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        msgBoxUI.SetActive(false);
        tileBuyUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.instance.money >= 1000000)
        {
            rankSavePanel.SetActive(true);
            GameManager.instance.isScore = false;

            int time = (int)GameManager.instance.score;

            float m = time / 60;
            float s = time % 60;

            score.text = "Time:" + m.ToString("D2") + ":" + s.ToString("D2");
        }

        UpdateHealthUI();
        UpdateMoneyUI();
        UpdateTimeUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            Time.timeScale = pauseUI.activeSelf ? 0 : 1;
        }
    }

    public void OffPause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void UpdateHealthUI()
    {
        GameManager gameManager = GameManager.instance;

        float value = gameManager.health / gameManager.maxHealth;
        healthImage.fillAmount = value;
        healthText.text = "ÇÇ·Îµµ : " + (value * 100).ToString("N1") + "%";
    }

    private void UpdateTimeUI()
    {
        GameManager gameManager = GameManager.instance;

        timeText.text = gameManager.hour.ToString("D2") + ":" + gameManager.minute.ToString("D2");
        dayText.text = "Day " + gameManager.day.ToString("");
    }

    private void UpdateMoneyUI()
    {
        GameManager gameManager = GameManager.instance;

        moneyText.text = "$ " + gameManager.money.ToString("#,###");
        moneyImage.fillAmount = gameManager.money / 1000000.0f;
    }

    public void SetTileUI(string text)
    {
        tileBuyText.text = text;
    }

    public void SetWeatherUI(string weather)
    {
        weatherText.text = weather;
    }

    public void ShowMsg(string msg)
    {
        msgBoxUI.SetActive(true);
        msgText.text = msg;

        StartCoroutine(WaitClose(msgBoxUI));
    }

    IEnumerator WaitClose(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        obj.SetActive(false);
    }

    public void SaveRankScore()
    {
        if (field.text == "")
            return;

        int time = (int)GameManager.instance.score;
        RankingManager.Instance.AddScore(time, field.text);
    }
}
