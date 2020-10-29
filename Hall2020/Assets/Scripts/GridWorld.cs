using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld<T> : Grid<T>
{
    Vector2 originPosition;
    int cellSize;
    public GridWorld(int width, int height, int cellSize, Vector2 originPosition) : base(width, height) {
        this.cellSize = cellSize;
        this.originPosition = originPosition;
    }

    internal Vector3 GetWorldPosition(int x, int y) {
        return (originPosition + new Vector2(cellSize * x, cellSize * y));
    }
}
