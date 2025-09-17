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
            Debug.Log($"{crop.seedType} 씨앗을 구매하여 창고에 저장했습니다!");
        }
        else
        {
            Debug.Log("돈이 부족합니다!");
        }
    }

    public void BuyTool(Tool tool, int price)
    {
        ToolInventory toolInv = FindObjectOfType<ToolInventory>();

        if (toolInv.inventory.Contains(tool))
        {
            Debug.Log("이미 해당 도구를 가지고 있습니다!");
            return;
        }

        if (GameManager.instance.money >= price)
        {
            GameManager.instance.money -= price;
            toolInv.AddItem(tool);
            Debug.Log($"{tool}을 구매했습니다!");
        }
        else
        {
            Debug.Log("돈이 부족합니다!");
        }
    }

    public void BuyTile(Vector3 position)
    {
        TileManager.instance.TryBuyTile(position);
    }
}
