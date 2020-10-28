using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;
    [SerializeField] Transform linesParent;
    internal void DrawWorldGrid<T>(GridWorld<T> worldGrid) {
        Vector3[] positions = new Vector3[2];

        for (int x = 0; x < worldGrid.width; x++) {
            positions[0] = worldGrid.GetWorldPosition(x, 0);
            positions[1] = worldGrid.GetWorldPosition(x, worldGrid.height);

            DrawLine(positions);          
        }

        for (int y = 0; y < worldGrid.height; y++) {
            positions[0] = worldGrid.GetWorldPosition(0, y);
            positions[1] = worldGrid.GetWorldPosition(worldGrid.width, y);

            DrawLine(positions);
        }

        //Top Horizontal Line
        positions[0] = worldGrid.GetWorldPosition(0, worldGrid.height);
        positions[1] = worldGrid.GetWorldPosition(worldGrid.width, worldGrid.height);

        DrawLine(positions);

        //Right-Most Vertical Line
        positions[0] = worldGrid.GetWorldPosition(worldGrid.width, 0);
        positions[1] = worldGrid.GetWorldPosition(worldGrid.width, worldGrid.height);

        DrawLine(positions);
    }

    private void DrawLine(Vector3[] positions) {
        GameObject line = Instantiate(linePrefab, linesParent);
        LineRenderer lr = line.GetComponentInChildren<LineRenderer>();

        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }
}
