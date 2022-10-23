using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : SortBase
{

    public override void StartSort()
    {
        if (currentCotoutine != null)
        {
            StartCoroutine(coroutine);
            return;
        }
        coroutine = OnBubble();
        currentCotoutine = StartCoroutine(coroutine);
    }

    IEnumerator OnBubble()
    {
        for (int i = 0; i < elementList.Count - 1; i++)
        {
            for (int j = 0; j < elementList.Count - i - 1; j++)
            {
                if(elementList[j].size > elementList[j + 1].size)
                {
                    elementList[j].ToggleColor(Define.ColorType.Boss);
                    elementList[j + 1].ToggleColor(Define.ColorType.Compare);

                    yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

                    Vector3 tempPos = elementList[j].temp.transform.position;
                    elementList[j].temp.transform.position = elementList[j + 1].temp.transform.position;
                    elementList[j + 1].temp.transform.position = tempPos;

                    Element tempElement = elementList[j];
                    elementList[j] = elementList[j + 1];
                    elementList[j + 1] = tempElement;

                    elementList[j].ToggleColor();
                    elementList[j + 1].ToggleColor();
                }
            }
        }
    }
}
