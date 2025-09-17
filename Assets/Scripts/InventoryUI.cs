using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public CropInventory cropInventory;
    public Transform slotParent;
    public GameObject slotPrefab;

    public static InventoryUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshUI()
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        for (int i = 0; i < cropInventory.inventory.Count; i++)
        {
            int index = i;
            var item = cropInventory.inventory[i];

            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponentInChildren<Text>().text =
                item.data.seedType.ToString() + (item.IsSeed ? " 씨앗" : " 작물");

            Button[] buttons = slot.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(() => MoveToStorage(index));
            buttons[1].onClick.AddListener(() => SellItem(index));
        }
    }

    void MoveToStorage(int index)
    {
        if (index < 0 || index >= cropInventory.inventory.Count) return;
        var item = cropInventory.inventory[index];
        if (Storage.instance.AddToStorage(item))
        {
            cropInventory.inventory.RemoveAt(index);
            Debug.Log($"{item.data.seedType} 창고로 이동");
        }

        RefreshUI();
        StorageUI.Instance.RefreshUI();
    }

    void SellItem(int index)
    {
        if (index < 0 || index >= cropInventory.inventory.Count) return;
        var item = cropInventory.inventory[index];
        int price = 0;

        if (item.IsSeed)
            return;

        for (int i = 0; i < GameManager.instance.cropDataList.Length; i++)
        {
            if (item.data.seedType == GameManager.instance.cropDataList[i].seedType)
            {
                price = GameManager.instance.price[i];
            }
        }

        GameManager.instance.money += price;
        cropInventory.inventory.RemoveAt(index);
        Debug.Log($"{item.data.seedType} 판매! +{price}$");

        RefreshUI();
    }
}
