using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraveManager : MonoBehaviour
{
    public string[] names;

    private Grave[] graves;
    private string[] donors;
    private bool[] collected;

    public GameObject donorsRoot;
    
    private void Awake()
    {
        graves = FindObjectsOfType<Grave>();
        donors = new string[7];
        collected = new bool[7];
        
        ShuffleNames();

        for (int i = 0; i < graves.Length; i++)
        {
            graves[i].SetOwner(names[i % names.Length]);
        }

        for (int i = 0; i < 7; i++) donors[i] = names[Random.Range(0, names.Length)];
        for (int i = 0; i < 7; i++) collected[i] = false;
        
        UpdateDonorsDisplay();
    }

    public void UpdateDonorsDisplay()
    {
        Text[] texts = donorsRoot.GetComponentsInChildren<Text>();
        
        for(int i = 0; i < 7; i++)
        {
            texts[i].text = donors[i];
            texts[i].color = collected[i] ? Color.green : Color.black;
        }
    }

    public void Collected(string str)
    {
        for (int i = 0; i < 7; i++)
        {
            if (str == donors[i])
            {
                collected[i] = true;
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
}
