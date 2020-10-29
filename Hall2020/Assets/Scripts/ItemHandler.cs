using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public List<Item> items;

    private List<Item> collected;

    private void Start()
    {
        collected = new List<Item>(items.Count);
    }

    public Item CollectRandomItem()
    {
        if (Random.value > 0.25f) return null;

        int i = Random.Range(0, items.Count);
        items.RemoveAt(i);
        collected.Add(items[i]);

        return items[i];
    }
}
