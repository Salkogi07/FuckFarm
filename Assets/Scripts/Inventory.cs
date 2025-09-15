using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();

    void Start()
    {
        AddItem(ItemType.WateringCan, 1);
    }

    public void AddItem(ItemType type, int count)
    {
        if (items.ContainsKey(type))
        {
            items[type] += count;
        }
        else
        {
            items.Add(type, count);
        }
        Debug.Log($"{type} {count}�� ȹ��! ����: {items[type]}��");
    }

    public bool UseItem(ItemType type)
    {
        if (items.ContainsKey(type) && items[type] > 0)
        {
            if (type == ItemType.NormalSeed)
            {
                items[type]--;
                Debug.Log($"{type} ���! ���� ����: {items[type]}��");
            }
            return true;
        }
        Debug.Log($"{type} �������� �����մϴ�.");
        return false;
    }
}