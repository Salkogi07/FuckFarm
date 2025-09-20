using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    public GameObject tilePrefab;
    public List<FarmTile> ownedTiles = new List<FarmTile>();

    private int tileCount = 0;
    private float basePrice = 20000f;
    public float tileOffset = 6f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Vector3 startPos = transform.position;
        CreateTile(startPos); // ù Ÿ�� �ڵ� ����
    }

    public float GetNextTilePrice()
    {
        if (tileCount == 0) return 0;
        return basePrice * Mathf.Pow(1.5f, tileCount - 1);
    }

    // ��ư���� ȣ���� �޼���
    public void TryBuyNextTile()
    {
        float price = GetNextTilePrice();

        if (GameManager.instance.money < price)
        {
            Debug.Log("�� ����!");
            return;
        }

        // Ȯ�� ������ ��ġ ã��
        Vector3? nextPos = FindExpandablePosition();
        if (nextPos == null)
        {
            Debug.Log("Ȯ���� ��ġ�� �����ϴ�!");
            return;
        }

        GameManager.instance.money -= price;
        CreateTile(nextPos.Value);
    }

    private void CreateTile(Vector3 position)
    {
        GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
        FarmTile farmTile = newTile.GetComponent<FarmTile>();

        ownedTiles.Add(farmTile);
        tileCount++;
    }

    private Vector3? FindExpandablePosition()
    {
        Vector3[] dirs = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        // 1. ó������ ���ڰ�(+) ��� Ȯ��
        if (tileCount <= 4)
        {
            Vector3 c = ownedTiles[0].transform.position;
            foreach (var d in dirs)
            {
                Vector3 pos = c + d * tileOffset;
                if (!IsTileOwned(pos)) return pos;
            }
        }

        // 2. �� �������ʹ� ���� Ÿ�� ���� ���� �ĺ� ����
        Vector3? bestPos = null;
        int bestCount = -1;

        foreach (var t in ownedTiles)
        {
            foreach (var d in dirs)
            {
                Vector3 pos = t.transform.position + d * tileOffset;
                if (IsTileOwned(pos)) continue;

                int count = CountAdj(pos);
                if (count > bestCount)
                {
                    bestCount = count;
                    bestPos = pos;
                }
            }
        }
        return bestPos;
    }

    private int CountAdj(Vector3 pos)
    {
        int c = 0;
        Vector3[] dirs = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
        foreach (var d in dirs)
            if (IsTileOwned(pos + d * tileOffset)) c++;
        return c;
    }




    private bool IsTileOwned(Vector3 pos)
    {
        foreach (var tile in ownedTiles)
        {
            if (tile.transform.position == pos)
                return true;
        }
        return false;
    }
}
