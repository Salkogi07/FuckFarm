using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class RankData
{
    public string name;
    public float score;
}

[Serializable]
public class RankList
{
    public List<RankData> ranks = new List<RankData>();
}

public class RankingManager : MonoBehaviour
{
    public static RankingManager Instance;

    public string fileName = "ranking.json";
    public int maxRank = 5;
    public bool isTimeMode = true;

    private RankList rankList = new RankList();

    private string FilePath => Path.Combine(Application.dataPath, "../" + fileName);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadRanking();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadRanking();
    }

    public void AddScore(float score, string playerName)
    {
        rankList.ranks.Add(new RankData { name = playerName, score = score });

        if (isTimeMode)
        {
            // �ð� ��� �� ���� ���� ����
            rankList.ranks.Sort((a, b) => a.score.CompareTo(b.score));
        }
        else
        {
            // ���� ��� �� ū ���� ����
            rankList.ranks.Sort((a, b) => b.score.CompareTo(a.score));
        }

        if (rankList.ranks.Count > maxRank)
            rankList.ranks.RemoveAt(rankList.ranks.Count - 1);

        SaveRanking();
    }

    public List<RankData> GetRanking()
    {
        LoadRanking();

        return rankList.ranks;
    }

    void SaveRanking()
    {
        string json = JsonUtility.ToJson(rankList, true);
        File.WriteAllText(FilePath, json);
    }

    void LoadRanking()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            rankList = JsonUtility.FromJson<RankList>(json);
        }
        else
        {
            rankList = new RankList();
        }
    }
}
