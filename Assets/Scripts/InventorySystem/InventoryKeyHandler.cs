using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryKeyHandler : MonoBehaviour
{
    // Variables
    [HideInInspector] public bool isShowing = true;
    public GameObject inventory;
    public KeyCode inventoryKey = KeyCode.E;
    public int hotbarSlots = 8;

    // Start is called before the first frame update
    void Start()
    {
        // Start the inventory closed
        CloseInventory();

        Debug.Log("Inventory system is active.");
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the inventory key to be pressed
        if (Input.GetKeyDown(inventoryKey))
        {
            if (inventory.activeSelf)
            {
                // If it is showing, hide it
                CloseInventory();
                InventoryManager.instance.ReselectPreviousSlot();
            }
            else
            {
                ShowInventory();
                InventoryManager.instance.DeselectAllSlots();
            }
        }

        // Only check keys for the hotbar if the inventory is closed
        if (!isShowing)
        {
            // Check for the scroll wheel to be scrolled
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                // Scroll up
                InventoryManager.instance.ChangeSelectedSlot("right");
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                // Scroll down
                InventoryManager.instance.ChangeSelectedSlot("left");
            }

            // Check for the number keys to be pressed
            for (int i = 1; i <= hotbarSlots; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    InventoryManager.instance.ChangeSelectedSlot(i - 1, InventoryManager.instance.selectedSlot);
                }
            }
        }
    }

    /// <summary>
    /// Closes the inventory menu. Also disables dragging and right-clicking for the hotbar items.
    /// </summary>
    public void CloseInventory()
    {
        inventory.SetActive(false);
        isShowing = false;

        // Disable dragging for the hotbar items
        InventoryManager.instance.GetComponent<InventoryManager>().SetDraggable(false);

        // Remove any right click menus if there are any
        try
        {
            Destroy(GameObject.Find("ItemTooltip(Clone)"));
        }
        catch (System.Exception) { }
    }

    /// <summary>
    /// Opens the inventory menu. Also enables dragging and right-clicking for the hotbar items.
    /// </summary>
    public void ShowInventory()
    {
        inventory.SetActive(true);
        isShowing = true;

        // Enable dragging for the hotbar items
        InventoryManager.instance.GetComponent<InventoryManager>().SetDraggable(true);
    }
}
