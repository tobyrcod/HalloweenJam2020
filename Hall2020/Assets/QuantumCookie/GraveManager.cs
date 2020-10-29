using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveManager : MonoBehaviour
{
    public string[] names;

    private Grave[] graves;

    private void Awake()
    {
        graves = FindObjectsOfType<Grave>();
        Debug.Log(graves.Length);
    }
}
