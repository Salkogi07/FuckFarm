using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{
    public Storage storage;
    public Transform slotParent;
    public GameObject slotPrefab;

    void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        foreach (var item in storage.storedItems)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponentInChildren<Text>().text =
                item.data.seedType.ToString() + (item.IsSeed ? " ¾¾¾Ñ" : " ÀÛ¹°");
        }
    }
}
