using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{
    public Storage storage;
    public Transform slotParent;
    public GameObject slotPrefab;

    public static StorageUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshUI()
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        for (int i = 0; i < storage.storedItems.Count; i++)
        {
            int index = i;
            var item = storage.storedItems[i];

            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponentInChildren<Text>().text =
                item.data.seedType.ToString() + (item.IsSeed ? " ¾¾¾Ñ" : " ÀÛ¹°");

            Button[] buttons = slot.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(() => TakeOut(index));
            buttons[1].onClick.AddListener(() => SellItem(index));
        }
    }

    void TakeOut(int index)
    {
        if (index < 0 || index >= storage.storedItems.Count) return;

        var item = storage.storedItems[index];
        if (InventoryUI.Instance.cropInventory.inventory.Count <
            InventoryUI.Instance.cropInventory.startItem.Length)
        {
            InventoryUI.Instance.cropInventory.inventory.Add(item);
            storage.storedItems.RemoveAt(index);
            Debug.Log($"{item.data.seedType} ÀÎº¥Åä¸®·Î ²¨³¿");
        }

        RefreshUI();
        InventoryUI.Instance.RefreshUI();
    }

    void SellItem(int index)
    {
        if (index < 0 || index >= storage.storedItems.Count) return;

        var item = storage.storedItems[index];
        if (item.IsSeed) return;

        int price = 0;
        for (int i = 0; i < GameManager.instance.cropDataList.Length; i++)
        {
            if (item.data.seedType == GameManager.instance.cropDataList[i].seedType)
            {
                price = GameManager.instance.price[i];
            }
        }

        GameManager.instance.money += price;
        storage.storedItems.RemoveAt(index);
        Debug.Log($"{item.data.seedType} ÆÇ¸Å! +{price}$");

        RefreshUI();
    }
}
