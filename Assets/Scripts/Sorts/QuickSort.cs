using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : SortBase
{
    int part;

    public override void StartSort()
    {
        if (currentCotoutine != null)
        {
            StartCoroutine(coroutine);
            return;
        }
        coroutine = OnQuick(0, elementList.Count - 1);
        currentCotoutine = StartCoroutine(coroutine);
    }

    IEnumerator OnQuick(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            elementList[i].ToggleColor(Define.ColorType.Booking);
        }

        yield return StartCoroutine(Partition(start, end));

        if (start < part - 1)
        {
            yield return StartCoroutine(OnQuick(start, part - 1));
        }
        if (part < end)
        {
            yield return StartCoroutine(OnQuick(part, end));
        }
    }

    IEnumerator Partition(int start, int end)
    {
        int pivotIndex = (start + end) / 2;
        int pivot = elementList[pivotIndex].size;
        elementList[pivotIndex].ToggleColor(Define.ColorType.Boss);
        yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

        while (start <= end)
        {
            while (elementList[start].size < pivot)
            {
                elementList[start].ToggleColor(Define.ColorType.Compare);
                yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);
                elementList[start].ToggleColor();

                start++;
            }
            elementList[start].ToggleColor(Define.ColorType.newBoss);

            while (elementList[end].size > pivot)
            {
                elementList[end].ToggleColor(Define.ColorType.Compare);
                yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);
                elementList[end].ToggleColor();

                end--;
            }
            elementList[end].ToggleColor(Define.ColorType.newBoss);

            yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            //if (start == pivotIndex)
            //{
            //    pivotIndex = end;
            //    elementList[start].ToggleColor(Define.ColorType.Boss);
            //}
            //else if (end == pivotIndex)
            //{
            //    pivotIndex = start;
            //    elementList[end].ToggleColor(Define.ColorType.Boss);
            //}

            //yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            if (start <= end)
            {
                Vector3 tempPos = elementList[start].temp.transform.position;

                elementList[start].temp.transform.position = elementList[end].temp.transform.position;
                elementList[end].temp.transform.position = tempPos;

                yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

                elementList[start].ToggleColor();
                elementList[end].ToggleColor();

                Element tempEle = elementList[start];
                elementList[start] = elementList[end];
                elementList[end] = tempEle;

                start++;
                end--;
            }
            else
            {
                elementList[start].ToggleColor();
                elementList[end].ToggleColor();
            }
        }
        part = start;
    }
}
