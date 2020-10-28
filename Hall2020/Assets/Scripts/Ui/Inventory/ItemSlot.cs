using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Item item;
    [SerializeField] private Image image;

    internal void UpdateItem(Item newItem) {
        item = newItem;
        if (item == null) {
            image.enabled = false;
        }
        else {
            image.sprite = item.icon;
            image.enabled = true;
        }
    }
}
