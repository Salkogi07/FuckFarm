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

    /// 현재 창고 용량
    public int GetCapacity()
    {
        return capacityLevels[upgradeLevel];
    }

    /// 아이템 추가
    public bool AddToStorage(Item item)
    {
        if (storedItems.Count >= GetCapacity())
        {
            Debug.Log("창고가 가득 찼습니다!");
            return false;
        }
        storedItems.Add(item);
        return true;
    }

    /// 아이템 제거 (씨앗 꺼내기 등)
    public bool RemoveFromStorage(Item item)
    {
        return storedItems.Remove(item);
    }

    /// 업그레이드
    public void UpgradeStorage()
    {
        if (upgradeLevel < capacityLevels.Length - 1)
        {
            upgradeLevel++;
            Debug.Log($"창고 업그레이드 완료! 현재 용량: {GetCapacity()}");
        }
        else
        {
            Debug.Log("창고 최대 업그레이드 상태입니다!");
        }
    }
}
