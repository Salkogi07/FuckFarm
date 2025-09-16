using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Crop
{
    ´ç±Ù¾¾¾Ñ,
    ºê·ÎÄÝ¸®¾¾¾Ñ,
    ÄÝ¸®ÇÃ¶ó¿ö¾¾¾Ñ,
    ¿Á¼ö¼ö¾¾¾Ñ,
    ÇØ¹Ù¶ó±â¾¾¾Ñ
}

public class CropInventory : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentCooldown <= 0)
            {
                UseItem();
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && inventory.Count > 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll < 0) // ÈÙ ¾Æ·¡·Î
            {
                select = (select + 1) % inventory.Count;
            }
            if (scroll > 0) // ÈÙ À§·Î
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
            Item newItem = new Item { type = itemType };
            inventory.Add(newItem);
        }
    }

    void UseItem()
    {
        if (inventory.Count == 0)
        {
            return;
        }

        inventory.RemoveAt(select);

        if (select >= inventory.Count && inventory.Count > 0)
            select = inventory.Count - 1;

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
