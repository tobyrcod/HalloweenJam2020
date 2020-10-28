using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int maxSize { get; private set; }
    public Item[] items { get; private set; }

    public Action<int> OnItemsChangedEvent;

    public Inventory(int maxSize) {
        this.maxSize = maxSize;
        this.items = new Item[maxSize];
    }

    public bool AddItem(Item item) {
        for (int i = 0; i < maxSize; i++) {
            if (items[i] == null) {
                //Add Item
                items[i] = item;
                OnItemsChangedEvent?.Invoke(i);
                return true;
            }
        }

        //The inventory is full
        return false;
    }

    public void RemoveItem(int index) {
        if (items[index] != null) {
            items[index] = null;
            OnItemsChangedEvent?.Invoke(index);
        }
    }
}
