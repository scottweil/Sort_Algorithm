using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSort : SortBase
{
    public override void StartSort()
    {
        if (currentCotoutine != null)
        {
            StartCoroutine(coroutine);
            return;
        }
            coroutine = OnHeap();
            currentCotoutine = StartCoroutine(coroutine);
        //OnHeap();
    }

    IEnumerator OnHeap()
    {
        int length = elementList.Count;
        for (int i = 0; i < length; i++)
        {
            elementList[i].ToggleColor(Define.ColorType.Booking);
        }

        for (int i = length / 2 - 1; i >= 0; i--)
        {
            int parentIndex = i;
            yield return StartCoroutine(Heapify(parentIndex, length));
        }

        for (int i = length - 1; i >= 0; i--)
        {
            elementList[0].ToggleColor(Define.ColorType.Boss);
            elementList[i].ToggleColor(Define.ColorType.Compare);

            yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            Vector3 tempPos = elementList[0].temp.transform.position;
            elementList[0].temp.transform.position = elementList[i].temp.transform.position;
            elementList[i].temp.transform.position = tempPos;

            Element tempEle = elementList[0];
            elementList[0] = elementList[i];
            elementList[i] = tempEle;

            yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            elementList[i].ToggleColor();
            elementList[0].ToggleColor(Define.ColorType.Booking);

            yield return StartCoroutine(Heapify(0, i));
        }
        elementList[0].ToggleColor();
    }

    IEnumerator Heapify(int parentIndex, int Length)
    {
        int child_L = 2 * parentIndex + 1;
        int child_R = 2 * parentIndex + 2;
        int parentOrigin = parentIndex;

        if (child_L < Length && elementList[parentIndex].size < elementList[child_L].size) parentIndex = child_L;
        if (child_R < Length && elementList[parentIndex].size < elementList[child_R].size) parentIndex = child_R;


        if (parentIndex != parentOrigin)
        {
            yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            Vector3 tempPos = elementList[parentOrigin].temp.transform.position;
            elementList[parentOrigin].temp.transform.position = elementList[parentIndex].temp.transform.position;
            elementList[parentIndex].temp.transform.position = tempPos;

            Element tempEle = elementList[parentOrigin];
            elementList[parentOrigin] = elementList[parentIndex];
            elementList[parentIndex] = tempEle;

            yield return StartCoroutine(Heapify(parentIndex, Length));
        }
    }
}
