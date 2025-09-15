using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    public GameObject playerSleep;
    public Transform sleepPos;

    public GameObject sleepUI;
    public Slider sleepSlider;
    public Text sleepText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            playerSleep.SetActive(true);
            sleepUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.SetActive(true);
            playerSleep.SetActive(false);
            sleepUI.SetActive(false);
        }
    }
}
