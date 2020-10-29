using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public Item[] items;

    private List<Item> collected;

    private void Start()
    {
        collected = new List<Item>(items.Length);
    }

    public Item CollectRandomItem()
    {
        if (Random.value > 0.25f) return null;

        int i = Random.Range(0, items.Length);
        collected.Add(items[i]);

        return items[i];
    }
}
