using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage instance;

    private int upgradeLevel = 0;
    private int[] capacityLevels = { 20, 40, 80 };

    public List<Item> storedItems = new List<Item>();

    private void Awake()
    {
        instance = this;
    }

    /// ���� â�� �뷮
    public int GetCapacity()
    {
        return capacityLevels[upgradeLevel];
    }

    /// ������ �߰�
    public bool AddToStorage(Item item)
    {
        if (storedItems.Count >= GetCapacity())
        {
            Debug.Log("â�� ���� á���ϴ�!");
            return false;
        }
        storedItems.Add(item);
        return true;
    }

    /// ������ ���� (���� ������ ��)
    public bool RemoveFromStorage(Item item)
    {
        return storedItems.Remove(item);
    }

    /// ���׷��̵�
    public void UpgradeStorage()
    {
        if (upgradeLevel < capacityLevels.Length - 1)
        {
            upgradeLevel++;
            Debug.Log($"â�� ���׷��̵� �Ϸ�! ���� �뷮: {GetCapacity()}");
        }
        else
        {
            Debug.Log("â�� �ִ� ���׷��̵� �����Դϴ�!");
        }
    }
}
