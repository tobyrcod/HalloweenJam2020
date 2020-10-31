using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManagerMain : MonoBehaviour
{
    public static PopUpManagerMain instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public GameObject prbMessageBox;
    public GameObject prbMessageBoxYES;
    public Canvas canvas;
}
