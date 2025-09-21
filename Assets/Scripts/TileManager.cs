using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public GameObject tilePrefab;
    public List<FarmTile> ownedTiles = new List<FarmTile>();

    private float basePrice = 20000f;
    private float tileOffset = 6f;
    private Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

    void Awake() => instance = this;
    void Start() => CreateTile(transform.position);

    public float GetNextTilePrice() =>
        ownedTiles.Count == 1 ? basePrice : basePrice * Mathf.Pow(1.5f, ownedTiles.Count - 1);

    public void TryBuyNextTile()
    {
        Debug.Log(GetNextTilePrice());

        float price = GetNextTilePrice();
        if (GameManager.instance.money < price) return;

        Vector3? nextPos = FindExpandablePosition();
        if (nextPos == null) return;

        GameManager.instance.money -= price;
        CreateTile(nextPos.Value);
    }

    private void CreateTile(Vector3 position)
    {
        GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
        ownedTiles.Add(newTile.GetComponent<FarmTile>());
    }

    private Vector3? FindExpandablePosition()
    {
        // 첫 타일 기준 십자 확장 (최대 4개까지)
        if (ownedTiles.Count <= 4)
        {
            Vector3 center = ownedTiles[0].transform.position;
            foreach (var dir in directions)
            {
                Vector3 pos = center + dir * tileOffset;
                if (!IsTileOwned(pos)) return pos;
            }
        }

        // 인접 타일이 가장 많은 위치 찾기
        Vector3? bestPos = null;
        int maxAdjacent = -1;

        foreach (var tile in ownedTiles)
        {
            foreach (var dir in directions)
            {
                Vector3 pos = tile.transform.position + dir * tileOffset;
                if (IsTileOwned(pos)) continue;

                int adjacentCount = CountAdjacent(pos);
                if (adjacentCount > maxAdjacent)
                {
                    maxAdjacent = adjacentCount;
                    bestPos = pos;
                }
            }
        }
        return bestPos;
    }

    private int CountAdjacent(Vector3 pos)
    {
        int count = 0;
        foreach (var dir in directions)
            if (IsTileOwned(pos + dir * tileOffset)) count++;
        return count;
    }

    private bool IsTileOwned(Vector3 pos) =>
        ownedTiles.Exists(tile => tile.transform.position == pos);
}
