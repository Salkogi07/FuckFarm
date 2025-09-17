using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public CropData data;
    public bool IsSeed;

    public Item(CropData data, bool isSeed)
    {
        this.data = data;
        this.IsSeed = isSeed;
    }
}

public class CropInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    [SerializeField] private int maxInventorySlots = 20;
    [SerializeField] private float itemCooldown = .5f;

    private float currentCooldown;
    private int select;

    public Farm currentFarm;
    public Item[] startItem;

    [Header("UI")]
    public Image image;
    public Text text;

    private void Start()
    {
        foreach (var item in startItem) AddItem(item);
    }

    private void Update()
    {
        UpdateCooldownUI();
        HandleInput();
        UpdateUI();
    }

    void UpdateCooldownUI()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            image.fillAmount = currentCooldown / itemCooldown;
        }
        else image.fillAmount = 0;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentCooldown <= 0) UseItem();
        if (Input.GetKey(KeyCode.LeftControl) && inventory.Count > 0)
            ScrollSelect(Input.GetAxis("Mouse ScrollWheel"));
    }

    void ScrollSelect(float scroll)
    {
        if (scroll == 0) return;
        select = (select + (scroll < 0 ? 1 : -1) + inventory.Count) % inventory.Count;
    }

    public void AddItem(Item newItem)
    {
        if (inventory.Count >= maxInventorySlots) return;
        inventory.Add(newItem);
        InventoryUI.Instance.RefreshUI();
        StorageUI.Instance.RefreshUI();
    }

    void UseItem()
    {
        if (inventory.Count == 0) return;
        Effect();
    }

    void Effect()
    {
        var item = inventory[select];
        if (!item.IsSeed || !currentFarm.PlantSeed(item.data)) return;

        inventory.RemoveAt(select);
        select = 0;
        currentCooldown = itemCooldown;

        InventoryUI.Instance.RefreshUI();
        StorageUI.Instance.RefreshUI();
    }

    void UpdateUI()
    {
        if (inventory.Count == 0) { text.text = ""; return; }
        var item = inventory[select];
        text.text = $"{item.data.seedType}{(item.IsSeed ? "¾¾¾Ñ" : "")}";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Farm")) currentFarm = other.GetComponent<Farm>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Farm")) currentFarm = null;
    }
}
