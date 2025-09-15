using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    public GameObject player;

    public GameObject playerSleep;
    public Transform sleepPos;

    public GameObject sleepUI;
    public Slider sleepSlider;
    public Text sleepText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;

            player.SetActive(false);
            playerSleep.SetActive(true);
            sleepUI.SetActive(true);
        }
    }

    private void Update()
    {
        sleepText.text = $"{sleepSlider.value} 시간 잠자기";
    }

    public void StartSleep()
    {
        float sleepHours = sleepSlider.value;
        StartCoroutine(SleepRoutine(sleepHours));
    }

    public void CancelSleep()
    {
        player.GetComponent<PlayerMove>().hasTarget = false;
        player.GetComponent<PlayerMove>().targetPositon = sleepPos.position;
        player.transform.position = sleepPos.position;

        player.SetActive(true);
        playerSleep.SetActive(false);
        sleepUI.SetActive(false);
        player = null;
    }

    IEnumerator SleepRoutine(float hours)
    {
        sleepUI.SetActive(false);

        float sleepTime = Mathf.Min(hours, 10f);
        int recoveryAmount = (int)(sleepTime * 10);

        float elapsedTime = 0f;
        float targetDuration = hours * 5;
        Time.timeScale = 10f;

        while (elapsedTime <= targetDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 1;
        CancelSleep();
        GameManager.instance.AddHealth(-recoveryAmount);
    }
}
