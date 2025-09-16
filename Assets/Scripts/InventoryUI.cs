using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public CropInventory cropInventory;
    public Transform slotParent;
    public GameObject slotPrefab;

    void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        // 기존 슬롯 제거
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        // 인벤토리 아이템 표시
        foreach (var item in cropInventory.inventory)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponentInChildren<Text>().text =
                item.data.seedType.ToString() + (item.IsSeed ? " 씨앗" : " 작물");
        }
    }
}
