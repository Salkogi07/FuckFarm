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
        // ���� ���� ����
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        // �κ��丮 ������ ǥ��
        foreach (var item in cropInventory.inventory)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponentInChildren<Text>().text =
                item.data.seedType.ToString() + (item.IsSeed ? " ����" : " �۹�");
        }
    }
}
