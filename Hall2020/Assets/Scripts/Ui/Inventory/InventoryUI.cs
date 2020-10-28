using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] Transform ItemSlotsParent;
    private ItemSlot[] itemSlots;
    private Inventory inventory;

    public void Init(Inventory inventory) {
        inventory.OnItemsChangedEvent += UpdateUI;
        this.inventory = inventory;

        itemSlots = new ItemSlot[inventory.maxSize];
        for (int i = 0; i < inventory.maxSize; i++) {
            itemSlots[i] = Instantiate(itemSlotPrefab, ItemSlotsParent).GetComponent<ItemSlot>();
            itemSlots[i].UpdateItem(null);
        }
    }

    public void UpdateUI(int index) {
        itemSlots[index].UpdateItem(inventory.items[index]);
    }
}
