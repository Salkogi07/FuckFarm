using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum Tool
{
    농사용일반도구,
    봉사용고급도구,
    일반물뿌리개,
    고급물뿌리개
}

public class ToolInventory : MonoBehaviour
{
    public class Item
    {
        public Tool type;
    }

    public Image image;
    public Text text;

    private List<Item> inventory = new List<Item>();
    private int maxInventorySlots = 20;

    private float itemCooldown = .5f;
    private float currentCooldown = 0f;

    private int select = 0;

    private void Start()
    {
        AddItem(Tool.농사용일반도구);
        AddItem(Tool.일반물뿌리개);
    }

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;

            float fill = currentCooldown / itemCooldown;
            image.fillAmount = fill;
        }
        else
        {
            image.fillAmount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentCooldown <= 0)
            {
                UseItem();
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && inventory.Count > 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll < 0) // 휠 아래로
            {
                select = (select + 1) % inventory.Count;
            }
            if (scroll > 0) // 휠 위로
            {
                select = (select - 1 + inventory.Count) % inventory.Count;
            }
        }

        UpdateUI();
    }

    public void AddItem(Tool itemType)
    {
        if (inventory.Count < maxInventorySlots)
        {
            Item newItem = new Item { type = itemType};
            inventory.Add(newItem);
        }
    }

    void UseItem()
    {
        if (inventory.Count == 0)
        {
            return;
        }

        currentCooldown = itemCooldown;
    }

    void UpdateUI()
    {
        if (inventory.Count == 0)
        {
            text.text = string.Empty;
            return;
        }

        text.text = inventory[select].type.ToString();
    }
}