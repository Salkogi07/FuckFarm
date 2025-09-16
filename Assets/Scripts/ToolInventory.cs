using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum Tool
{
    농사용일반도구,
    농사용고급도구,
    일반물뿌리개,
    고급물뿌리개
}

public class ToolInventory : MonoBehaviour
{
    public Image image;
    public Text text;

    private List<Tool> inventory = new List<Tool>();
    private int maxInventorySlots = 20;

    private float itemCooldown = .5f;
    private float currentCooldown = 0f;

    private int select = 0;

    public Farm currentFarm;
    public FarmTile currentTile;

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

        if (Input.GetKeyDown(KeyCode.X))
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

    public void AddItem(Tool newItem)
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

        currentCooldown = itemCooldown;
    }

    public void Effect()
    {
        switch (inventory[select])
        {
            case Tool.농사용일반도구:
                currentFarm.Plow();
                break;
            case Tool.농사용고급도구:
                currentFarm.Plow();
                break;
            case Tool.일반물뿌리개:
                currentTile.WaterTile(50);
                break;
            case Tool.고급물뿌리개:
                currentTile.WaterTile(20);
                break;
        }
    }

    void UpdateUI()
    {
        if (inventory.Count == 0)
            text.text = string.Empty;
        else
            text.text = inventory[select].ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Farm")
        {
            currentFarm = other.GetComponent<Farm>();
        }

        if(other.tag == "Tile")
        {
            currentTile = other.GetComponent<FarmTile>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Farm")
        {
            currentFarm = null;
        }

        if (other.tag == "Tile")
        {
            currentTile = null;
        }
    }
}