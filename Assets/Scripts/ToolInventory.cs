using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tool { 농사용일반도구, 농사용고급도구, 일반물뿌리개, 고급물뿌리개 }

public class ToolInventory : MonoBehaviour
{
    public Image image;
    public Text text;
    public List<Tool> inventory = new();

    [SerializeField] private int maxInventorySlots = 20;
    [SerializeField] private float itemCooldown = .5f;

    private float currentCooldown;
    private int select;

    public Farm currentFarm;
    public FarmTile currentTile;

    private void Start()
    {
        AddItem(Tool.농사용일반도구);
        AddItem(Tool.일반물뿌리개);
        AddItem(Tool.농사용고급도구);
        AddItem(Tool.고급물뿌리개);
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
        if (Input.GetKeyDown(KeyCode.X) && currentCooldown <= 0) UseItem();
        if (Input.GetKey(KeyCode.LeftShift) && inventory.Count > 0)
            ScrollSelect(Input.GetAxis("Mouse ScrollWheel"));
    }

    void ScrollSelect(float scroll)
    {
        if (scroll == 0) return;
        select = (select + (scroll < 0 ? 1 : -1) + inventory.Count) % inventory.Count;
    }

    public void AddItem(Tool newItem)
    {
        if (inventory.Count < maxInventorySlots) inventory.Add(newItem);
    }

    void UseItem()
    {
        if (inventory.Count == 0) return;
        Effect();
    }

    void Effect()
    {
        if (GameManager.instance.health >= 100) return;

        switch (inventory[select])
        {
            case Tool.농사용일반도구:
                if (!currentFarm.Plow(gameObject, false)) return;
                GameManager.instance.AddHealth(2);
                break;
            case Tool.농사용고급도구:
                if (!currentFarm.Plow(gameObject, true)) return;
                GameManager.instance.AddHealth(2);
                break;
            case Tool.일반물뿌리개:
                currentTile.WaterTile(50); GameManager.instance.AddHealth(3); break;
            case Tool.고급물뿌리개:
                currentTile.WaterTile(20); GameManager.instance.AddHealth(3); break;
        }
        currentCooldown = itemCooldown;
    }

    void UpdateUI()
    {
        text.text = inventory.Count == 0 ? "" : inventory[select].ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Farm")) currentFarm = other.GetComponent<Farm>();
        if (other.CompareTag("Tile")) currentTile = other.GetComponent<FarmTile>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Farm")) currentFarm = null;
        if (other.CompareTag("Tile")) currentTile = null;
    }
}
