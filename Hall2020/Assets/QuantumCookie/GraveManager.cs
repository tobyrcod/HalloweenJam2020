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

    public GameObject donorsRoot;


    private void Awake()
    {
        graves = FindObjectsOfType<Grave>();
        donors = new string[7];
        areCollected = new bool[7];
        
        collectedItems = new List<Item>(items.Count);
        
        ShuffleNames();

        for (int i = 0; i < graves.Length; i++)
        {
            graves[i].SetOwner(names[i % names.Length]);
        }

        for (int i = 0; i < 7; i++) donors[i] = names[Random.Range(0, names.Length)];
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

    public void Collected(string str)
    {
        for (int i = 0; i < 7; i++)
        {
            if (str == donors[i])
            {
                areCollected[i] = true;
                Debug.Log(("Collected"));
                UpdateDonorsDisplay();
                break;
            }
        }
    }

    private void ShuffleNames()
    {
        for (int i = names.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            
            string t = names[i];
            names[i] = names[j];
            names[j] = t;
        }
    }

    public Item CollectRandomItem(string graveName)
    {
        if(donors.Contains(graveName))
        {
            int i = Random.Range(0, items.Count);
            items.RemoveAt(i);
            collectedItems.Add(items[i]);

            return items[i];
        }

        return null;
    }
}
