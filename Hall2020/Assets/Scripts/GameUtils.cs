using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils : MonoBehaviour
{
    public static GameUtils instance;

    private void Awake() {
        instance = this;
    }

    public Item ArmItem;
    public Item LegItem;

    public List<Item> MiscItems;

    public Item[] GetRandomItems(int itemCount) {
        Item[] items = new Item[itemCount];

        //Some code to randomly generate items
        //For now just return either the arm or the leg

        int rnd = Random.Range(0, 2);
        if (rnd == 0) {
            items[0] = ArmItem;
        }
        else {
            items[0] = LegItem;
        }

        return items;
    }
}
