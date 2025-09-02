using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType fitType;
    
    public int rows;
    public int collums;
    public Vector2 cellSize;

    public Vector2 spacing;

    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
            
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            collums = Mathf.CeilToInt(sqrRt);

        }
        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)collums);
        }

        if (fitType == FitType.Height  || fitType == FitType.FixedRows)
        {
            collums = Mathf.CeilToInt(transform.childCount / (float)rows);
        }
        
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)collums - ((spacing.x / (float)collums) * 2) - (padding.left / (float) collums) - (padding.right / (float) collums);
        float celLHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float) rows) - (padding.bottom / (float) rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? celLHeight : cellSize.y;

        int collumnCount = 0;
        int rowsCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowsCount = i / collums;
            collumnCount = i % collums;

            var item = rectChildren[i];

            var xPos = (cellSize.x * collumnCount) + (spacing.x * collumnCount) + padding.left;
            var yPos = (cellSize.y * rowsCount) + (spacing.y * rowsCount) + padding.top;
            
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);

            
        }
    }
    
    public override void CalculateLayoutInputVertical()
    {
    }
    
    public override void SetLayoutHorizontal()
    {
    }
    
    public override void SetLayoutVertical()
    {
    }
}
