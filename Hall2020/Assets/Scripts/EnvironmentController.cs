using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] int cellSize;
    [SerializeField] Vector2 originPosition;

    [Space]

    [SerializeField] GridUI gridUI;
    GridWorld<bool> worldGrid;

    private void Awake() {
        worldGrid = new GridWorld<bool>(width, height, cellSize, originPosition);
        gridUI.DrawWorldGrid(worldGrid);
    }
}
