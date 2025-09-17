using UnityEngine;

public class Store : MonoBehaviour
{
    public void BuySeed(CropData crop, int price)
    {
        if (GameManager.instance.money >= price)
        {
            GameManager.instance.money -= price;
            Item newItem = new Item(crop, true);
            Storage.instance.AddToStorage(newItem);
            Debug.Log($"{crop.seedType} ������ �����Ͽ� â�� �����߽��ϴ�!");
        }
        else
        {
            Debug.Log("���� �����մϴ�!");
        }
    }

    public void BuyTool(Tool tool, int price)
    {
        ToolInventory toolInv = FindObjectOfType<ToolInventory>();

        if (toolInv.inventory.Contains(tool))
        {
            Debug.Log("�̹� �ش� ������ ������ �ֽ��ϴ�!");
            return;
        }

        if (GameManager.instance.money >= price)
        {
            GameManager.instance.money -= price;
            toolInv.AddItem(tool);
            Debug.Log($"{tool}�� �����߽��ϴ�!");
        }
        else
        {
            Debug.Log("���� �����մϴ�!");
        }
    }

    public void BuyTile(Vector3 position)
    {
        TileManager.instance.TryBuyTile(position);
    }
}
