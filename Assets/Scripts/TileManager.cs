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
        CreateTile(startPos); // 첫 타일 자동 생성
    }

    public float GetNextTilePrice()
    {
        if (tileCount == 0) return 0;
        return basePrice * Mathf.Pow(1.5f, tileCount - 1);
    }

    // 버튼에서 호출할 메서드
    public void TryBuyNextTile()
    {
        float price = GetNextTilePrice();

        if (GameManager.instance.money < price)
        {
            Debug.Log("돈 부족!");
            return;
        }

        // 확장 가능한 위치 찾기
        Vector3? nextPos = FindExpandablePosition();
        if (nextPos == null)
        {
            Debug.Log("확장할 위치가 없습니다!");
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

        // 1. 처음에는 십자가(+) 모양 확장
        if (tileCount <= 4)
        {
            Vector3 c = ownedTiles[0].transform.position;
            foreach (var d in dirs)
            {
                Vector3 pos = c + d * tileOffset;
                if (!IsTileOwned(pos)) return pos;
            }
        }

        // 2. 그 다음부터는 인접 타일 개수 많은 후보 선택
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
