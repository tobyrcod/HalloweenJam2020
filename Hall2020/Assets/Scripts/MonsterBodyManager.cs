using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBodyManager : MonoBehaviour
{
    public GameObject[] parts;

    private void Awake()
    {
        parts = new GameObject[7];
    }

    public void SetVisible(int i)
    {
        parts[i].SetActive(true);
    }
}