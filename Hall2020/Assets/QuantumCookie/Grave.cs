using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grave : MonoBehaviour
{
    public string owner;
    public bool fresh = true;

    public Text display;
    
    public void SetOwner(string n)
    {
        owner = n;
        display.text = owner;
    }
}