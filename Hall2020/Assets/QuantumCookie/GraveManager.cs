using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GraveManager : MonoBehaviour
{
    public string[] names;


    public List<Item> items;
    private List<Item> collectedItems;
    private Grave[] graves;
    private string[] donors;
    private bool[] areCollected;
    private int[] indices;

    public GameObject donorsRoot;

    private Item currentItem;

    private void Awake()
    {
        graves = FindObjectsOfType<Grave>();
        indices = new int[graves.Length];
        donors = new string[7];
        areCollected = new bool[7];

        currentItem = null;

        collectedItems = new List<Item>(items.Count);

        Shuffle(names);

        for (int i = 0; i < graves.Length; i++)
        {
            graves[i].SetOwner(names[i]);
        }

        for (int i = 0; i < indices.Length; i++) indices[i] = i;
        Shuffle(indices);

        for (int i = 0; i < 7; i++) donors[i] = graves[indices[i]].owner;
        for (int i = 0; i < 7; i++) areCollected[i] = false;
        
        UpdateDonorsDisplay();
    }

    public void UpdateDonorsDisplay()
    {
        Text[] texts = donorsRoot.GetComponentsInChildren<Text>();
        
        for(int i = 0; i < 7; i++)
        {
            texts[i].text = donors[i];
            texts[i].color = areCollected[i] ? Color.green : Color.black;
        }
    }

    public void Collected(string str, Item item)
    {
        for (int i = 0; i < 7; i++)
        {
            if (str == donors[i])
            {
                areCollected[i] = true;
                Debug.Log(("Collected"));
                currentItem = item;
                UpdateDonorsDisplay();
                break;
            }
        }
    }

    public bool HasItem()
    {
        return currentItem != null;
    }

    public Item CurrentItem()
    {
        return currentItem;
    }
    
    public void FixedItem()
    {
        currentItem = null;
        UpdateDonorsDisplay();
    }

    private void Shuffle(string[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            
            string t = arr[i];
            arr[i] = arr[j];
            arr[j] = t;
        }
    }

    private void Shuffle(int[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            int t = arr[i];
            arr[i] = arr[j];
            arr[j] = t;
        }
    }

    public Item CollectRandomItem(string graveName)
    {
        if(donors.Contains(graveName))
        {
            int i = Random.Range(0, items.Count);
            Item item = items[i];
            collectedItems.Add(item);
            items.RemoveAt(i);

            return item;
        }

        return null;
    }
}
