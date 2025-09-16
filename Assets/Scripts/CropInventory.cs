using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public CropData data;
    public bool IsSeed = false;

    public Item(CropData data , bool value)
    {
        this.data = data;
        this.IsSeed = value;
    }
}

public class CropInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    private int maxInventorySlots = 20;

    private float itemCooldown = .5f;
    private float currentCooldown = 0f;

    private int select = 0;

    public Farm currentFarm;

    public Item[] startItem;

    [Header("UI")]
    public Image image;
    public Text text;

    private void Start()
    {
        foreach (var item in startItem)
        {
            AddItem(item);
        }
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

        if (Input.GetKey(KeyCode.LeftControl) && inventory.Count > 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll < 0) // »Ÿ æ∆∑°∑Œ
            {
                select = (select + 1) % inventory.Count;
            }
            if (scroll > 0) // »Ÿ ¿ß∑Œ
            {
                select = (select - 1 + inventory.Count) % inventory.Count;
            }
        }

        UpdateUI();
    }

    public void AddItem(Item newItem)
    {
        if (inventory.Count < maxInventorySlots)
        {
            inventory.Add(newItem);
        }
    }

    void UseItem()
    {
        if (inventory.Count == 0)
            return;

        Effect();
    }

    public void Effect()
    {
        if (!inventory[select].IsSeed)
            return;

        if (!currentFarm.PlantSeed(inventory[select].data))
            return;

        inventory.RemoveAt(select);
        select = 0;
        currentCooldown = itemCooldown;
    }

    void UpdateUI()
    {
        if (inventory.Count == 0)
            text.text = string.Empty;
        else
        {
            text.text = inventory[select].data.seedType.ToString();
            if (inventory[select].IsSeed)
                text.text += "æææ—";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Farm")
        {
            currentFarm = other.GetComponent<Farm>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Farm")
        {
            currentFarm = null;
        }
    }
}
