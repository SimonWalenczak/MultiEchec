using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grid<TGridObject>
{
    public TGridObject[,] GridArray;
    public int Width;
    public int Height;
    public float CellSize;

    private Vector3 _originPos;
    private bool _showDebug = true;

    public Grid(int width, int height, float cellSize, Vector3 originPos, Func<Grid<TGridObject>, int, int, TGridObject> createGrid, Transform parent = null)
    {
        this.Width = width;
        this.Height = height;
        this.CellSize = cellSize;
        _originPos = originPos;

        GridArray = new TGridObject[width, height];
        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int y = 0; y < GridArray.GetLength(1); y++)
            {
                GridArray[x, y] = createGrid(this, x, y);
            }
        }

        if (_showDebug == false)
        {
            return;
        }

        #region Debug
        {
            for (int x = 0; x < GridArray.GetLength(0); x++)
            {
                for (int y = 0; y < GridArray.GetLength(1); y++)
                {
                    //apth node
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }

            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
        #endregion
    }


    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize + _originPos;
    }
    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - _originPos).x / CellSize);
        y = Mathf.FloorToInt((worldPos - _originPos).y / CellSize);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            return GridArray[x, y];
        }

        return default;
    }
    public float GetCellSize()
    {
        return CellSize;
    }
    public int GetWidth()
    {
        return GridArray.GetLength(0);
    }
    public int GetHeight()
    {
        return GridArray.GetLength(1);
    }
}
