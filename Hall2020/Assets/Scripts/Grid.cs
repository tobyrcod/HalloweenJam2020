using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T> {

    public int width { get; private set; }
    public int height { get; private set; }
    T[,] items;

    public Grid(int width, int height) {
        this.width = width;
        this.height = height;

        this.items = new T[width, height];
    }
}
