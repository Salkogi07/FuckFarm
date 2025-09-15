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
        Debug.Log($"{type} {count}개 획득! 현재: {items[type]}개");
    }

    public bool UseItem(ItemType type)
    {
        if (items.ContainsKey(type) && items[type] > 0)
        {
            if (type == ItemType.NormalSeed)
            {
                items[type]--;
                Debug.Log($"{type} 사용! 남은 개수: {items[type]}개");
            }
            return true;
        }
        Debug.Log($"{type} 아이템이 부족합니다.");
        return false;
    }
}