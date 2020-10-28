using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory/Item")]
[Serializable]
public class Item : ScriptableObject
{
    public Sprite icon;
}
