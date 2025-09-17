using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void SceneMove(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void RankView()
    {
        RankingUI.instance.OpenPanel();
    }
}
