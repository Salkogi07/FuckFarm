using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropInventory : MonoBehaviour
{
    public Image image;
    public Text text;

    private List<CropData> inventory = new List<CropData>();
    private int maxInventorySlots = 20;

    private float itemCooldown = .5f;
    private float currentCooldown = 0f;

    private int select = 0;

    public Farm currentFarm;

    public CropData[] startItem;


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

    public void AddItem(CropData newItem)
    {
        if (inventory.Count < maxInventorySlots)
        {
            inventory.Add(newItem);
        }
    }

    void UseItem()
    {
        if (inventory.Count == 0)
        {
            return;
        }

        Effect();

        select = 0;

        currentCooldown = itemCooldown;
    }

    public void Effect()
    {
        currentFarm.PlantSeed(inventory[select]);
        inventory.RemoveAt(select);
    }

    void UpdateUI()
    {
        if (inventory.Count == 0)
            text.text = string.Empty;
        else
            text.text = inventory[select].seedType.ToString();
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
