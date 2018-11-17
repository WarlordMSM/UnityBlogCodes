using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutFitter : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public int columnMin, columnMax;
    public int totalItems;
    public float itemWidthHeightRatio = 1f;
    public float extraFittableAmount = 0f;
    public bool cropSides, cropUpAndDown;


    private void Start()
    {
        Fit();
    }

    [ContextMenu("Fit()")]
    private void Fit()
    {
        //public Dictionary<int,float> Co
        float rectWidth = gridLayoutGroup.GetComponent<RectTransform>().rect.width;
        float rectHeight = gridLayoutGroup.GetComponent<RectTransform>().rect.height;
        for (int i = columnMin; i <= columnMax; i++)
        {
            int rows = totalItems / i + (totalItems % i == 0 ? 0 : 1);
            float itemWidth = rectWidth / i;
            float itemHeight = itemWidth / itemWidthHeightRatio;

            float tmp = rows * itemHeight - rectHeight;
            if (tmp < extraFittableAmount)
            {
                if (tmp < -extraFittableAmount)
                {
                    gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;
                    //itemWidth = (rectWidth + extraFittableAmount) / i;
                    //itemHeight = itemWidth / itemWidthHeightRatio;
                    //if(itemWidth * rows > rectHeight)
                    //{
                    //    itemHeight = rectHeight / rows;
                    //    itemWidth = itemHeight * itemWidthHeightRatio;
                    //}
                }
                else if (tmp < 0f)
                {
                    gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;
                    itemHeight = rectHeight / rows;
                    itemWidth = itemHeight * itemWidthHeightRatio;
                }
                else //tmp > 0
                {
                    gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
                }
                gridLayoutGroup.cellSize = new Vector2(itemWidth, itemHeight);
                gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayoutGroup.constraintCount = i;
                break;
            }
        }
        //Debug.LogWarning("couldn't find any solution!");
    }


}
