using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStorage : MonoBehaviour
{
    public GameObject panel;

    public GameObject player;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                panel.SetActive(true);

                player = other.gameObject;
                player.GetComponent<PlayerMove>().hasTarget = false;
                player.GetComponent<PlayerMove>().animator.Play("Idle");
                player.GetComponent<PlayerMove>().isMove = false;
            }
        }
    }

    public void OffPanel()
    {
        panel.SetActive(false);
        player.GetComponent<PlayerMove>().isMove = true;
    }
}
