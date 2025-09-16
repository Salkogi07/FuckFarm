using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    public GameObject tilePrefab;
    public List<FarmTile> ownedTiles = new List<FarmTile>();

    private int tileCount = 0;
    private float basePrice = 20000f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Vector3 startPos = transform.position;
        CreateTile(startPos);
    }

    public float GetNextTilePrice()
    {
        if (tileCount == 0) return 0;
        return basePrice * Mathf.Pow(1.5f, tileCount - 1);
    }

    public void TryBuyTile(Vector3 position)
    {
        float price = GetNextTilePrice();

        if (GameManager.instance.money < price)
        {
            Debug.Log("�� ����!");
            return;
        }

        if (!IsAdjacentToOwnedTile(position))
        {
            Debug.Log("������ Ÿ���� �ƴ�!");
            return;
        }

        GameManager.instance.money -= price;
        CreateTile(position);
    }

    private void CreateTile(Vector3 position)
    {
        GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
        FarmTile farmTile = newTile.GetComponent<FarmTile>();

        ownedTiles.Add(farmTile);
        tileCount++;
    }

    private bool IsAdjacentToOwnedTile(Vector3 position)
    {
        foreach (var tile in ownedTiles)
        {
            Vector3 diff = tile.transform.position - position;

            // ��Ȯ�� �����¿� (�밢��X)
            if ((Mathf.Abs(diff.x) == 1f && diff.z == 0) ||
                (Mathf.Abs(diff.z) == 1f && diff.x == 0))
            {
                return true;
            }
        }
        return false;
    }
}
